using System.Collections.Generic;

namespace aspnetApi.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //Navigation properties used to define relationships

        public List<Book> Books { get; set; }
    }
}