using System.ComponentModel.DataAnnotations;

namespace aspnetApi.Models.ViewModels
{
    public class BookVM
    {
        [Required(ErrorMessage = "Title is mandatory field")]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
        [StringLength(100, MinimumLength = 20)]

        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }

        [RegularExpression("^[1-5]*$", ErrorMessage = "Rate must be numeric")]
        public int? Rate { get; set; }

        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Url]
        public string CoverUrl { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
    }

    public class BookWithAuthorsVM
    {
        [Required(ErrorMessage = "Title is mandatory field")]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Rate must be numeric")]
        public int? Rate { get; set; }

        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Url]
        public string CoverUrl { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
        public string PublisherName { get; set; }
        public List<string> AuthorNames { get; set; }
    }
}