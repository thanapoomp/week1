namespace week1.DTOs
{
    public class BookDTO_Filter : PaginationDto
    {
        public string Name { get; set; }
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = 999999;
    }
}