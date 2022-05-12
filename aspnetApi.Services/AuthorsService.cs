using aspnetApi.Data;
using aspnetApi.Models;
using aspnetApi.Models.ViewModels;

//To add book to database
namespace aspnetApi.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public Author AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
            return _author;
        }
        public void DeleteAuthorById(int AuthorId)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == AuthorId);
            if (_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();
            }
        }
        public Author GetAuthorbyId(int id) => _context.Authors.FirstOrDefault(n => n.Id == id);

        public AuthorWithBookVM GetAuthorWithBook(int AuthorId)
        {
            var _author = _context.Authors.Where(n => n.Id == AuthorId).Select(n => new AuthorWithBookVM()
            {
                FullName = n.FullName,
                BooksTitle = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();
            return _author;
        }
    }
}