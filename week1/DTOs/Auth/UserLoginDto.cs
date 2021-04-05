using System.ComponentModel.DataAnnotations;

namespace week1.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}