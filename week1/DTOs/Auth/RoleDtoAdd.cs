using week1.Validations;

namespace week1.DTOs
{
    public class RoleDtoAdd
    {
        [FirstLetterUpperCase]
        public string RoleName { get; set; }
    }
}