using System.Collections.Generic;

namespace week1.DTOs
{
    public class CustomerDTO_ToReturn_Summary
    {
        public double SumBalance { get; set; }
        public int CustomerCount { get; set; }
        public List<CustomerDTO_ToReturn> Detail { get; set; }
    }
}