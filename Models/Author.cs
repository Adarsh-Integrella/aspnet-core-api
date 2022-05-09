namespace aspnetApi.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigations properties
        public List<Book_Author> Book_Authors { get; set; }
    }
}