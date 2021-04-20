using System;

namespace week1.DTOs
{
    public class BookDTO_ToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}