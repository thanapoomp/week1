using System.ComponentModel.DataAnnotations;

namespace week1.DTOs
{
    public class BookDTO_ToCreate
    {
        [Required]
        [MinLength(10),MaxLength(20)]
        public string Name { get; set; }
        [Range(0,50000)]
        public double Price { get; set; }
    }
}