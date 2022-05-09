using System.ComponentModel.DataAnnotations;

namespace aspnetApi.Models.ViewModels
{
    public class PublisherVM
    {
         [Required(ErrorMessage = "Publisher fullname is mandatory field")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
    }
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVM> BookAuthors { get; set; }
    }
    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}