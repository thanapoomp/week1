using System.ComponentModel.DataAnnotations;

namespace week1.DTOs
{
    public class BookDTO_ToCreate
    {
        [Required]
        public string Name { get; set; }
        [Range(0,50000)]
        public double Price { get; set; }
    }
}