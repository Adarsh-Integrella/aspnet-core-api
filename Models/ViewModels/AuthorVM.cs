using System.ComponentModel.DataAnnotations;

namespace aspnetApi.Models.ViewModels
{
    public class AuthorVM
    {
         [Required(ErrorMessage = "Author fullname is mandatory field")]
        [StringLength(50, MinimumLength = 2)]
        public string FullName { get; set; }
    }
    public class AuthorWithBookVM
    {
        
        public string FullName { get; set; }
        public List<string> BooksTitle { get; set; }
    }
}