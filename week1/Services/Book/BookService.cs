using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using week1.Data;
using week1.DTOs;
using week1.Models;

namespace week1.Services.Book
{
    public class BookService : ServiceBase, IBookService
    {

        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuraton;

        /// Constructor
        public BookService(IHttpContextAccessor httpContext,
            AppDBContext db,
            IMapper mapper,
            IConfiguration configuraton) : base(db, mapper, httpContext)
        {
            _db = db;
            _mapper = mapper;
            _configuraton = configuraton;
        }

        public async Task<ServiceResponse<BookDTO_ToReturn>> Create(BookDTO_ToCreate bookDTO_ToCreate)
        {
            //Create book
            var bookToCreate = new Models.Book();
            bookToCreate.Name = bookDTO_ToCreate.Name;
            bookToCreate.Price = bookDTO_ToCreate.Price;
            bookToCreate.CreatedDate = DateTime.Now;

            try
            {
                await _db.AddAsync(bookToCreate);
                await _db.SaveChangesAsync();

                //Map Book to BookDTO_ToReturn
                var result = _mapper.Map<BookDTO_ToReturn>(bookToCreate);

                //Return Value
                return ResponseResult.Success(result);
            }
            catch (System.Exception ex)
            {
                return ResponseResult.Failure<BookDTO_ToReturn>(ex.Message);
            }
        }
    }
}