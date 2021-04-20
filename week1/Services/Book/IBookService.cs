using System.Threading.Tasks;
using week1.DTOs;
using week1.Models;

namespace week1.Services.Book
{
    public interface IBookService 
    {
          Task<ServiceResponse<BookDTO_ToReturn>> Create(BookDTO_ToCreate bookDTO_ToCreate);
    }
}