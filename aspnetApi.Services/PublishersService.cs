using System.Text.RegularExpressions;
using aspnetApi.Data;
using aspnetApi.Models;
using aspnetApi.Models.ViewModels;

//To add book to database
namespace aspnetApi.Services
{
    public class PublishersService
    {
        private readonly AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        //api/publishers/get-all-publisher?sortBy=name_desc
        public List<Publisher> GetAllPublishers(string sortBy, string searchString)
        {
            var allPublisher = _context.publishers.OrderBy(n => n.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublisher = allPublisher.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublisher = allPublisher.Where(n => n.Name.Contains(searchString)).ToList();
            }

            // Paging

            return allPublisher;
        }

        public Publisher AddPublisher(PublisherVM publishers)
        {
            var _publisher = new Publisher()
            {
                Name = publishers.Name
            };
            _context.publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.publishers.FirstOrDefault(n => n.Id == id);
        public void DeletePublisherById(int PublisherId)
        {
            var _publisher = _context.publishers.FirstOrDefault(n => n.Id == PublisherId);
            if (_publisher != null)
            {
                _context.publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with this {PublisherId} not available");
            }
        }
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int PublisherId)
        {
            var _publisherData = _context.publishers.Where(n => n.Id == PublisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _publisherData;
        }

    }
}