using System.ComponentModel.DataAnnotations.Schema;

namespace aspnetApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }

        //Navigation properties
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public List<Book_Author> Book_Authors { get; set; }


    }
}