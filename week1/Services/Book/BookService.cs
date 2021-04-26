using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using week1.Data;
using week1.DTOs;
using week1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using week1.Helpers;
using Microsoft.EntityFrameworkCore;

namespace week1.Services.Book
{
    public class BookService : ServiceBase, IBookService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuraton;

        /// Constructor
        public BookService(IHttpContextAccessor httpContext,
            AppDBContext db,
            IMapper mapper,
            IConfiguration configuraton) : base(db, mapper, httpContext)
        {
            this._httpContext = httpContext;
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

        public async Task<ServiceResponseWithPagination<List<BookDTO_ToReturn>>> SearchPaginate(BookDTO_Filter filter)
        {
            // 1. Filter => Search
            var queryable = _db.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                queryable = queryable.Where(x => (x.Name).Contains(filter.Name));
            }

            queryable = queryable.Where(x => x.Price >= filter.MinPrice);
            queryable = queryable.Where(x => x.Price <= filter.MaxPrice);

            
            // 2. Order => Order by
            if (!string.IsNullOrWhiteSpace(filter.OrderingField))
            {
                try
                {
                    queryable = queryable.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    return ResponseResultWithPagination.Failure<List<BookDTO_ToReturn>>($"Could not order by field: {filter.OrderingField}");
                }
            }


            // 3. Add Paginate => Page,total,Perpage
            var paginationResult = await _httpContext.HttpContext.InsertPaginationParametersInResponse(queryable, filter.RecordsPerPage, filter.Page);

            // 4. Execute Query
            var books = await queryable.Paginate(filter).ToListAsync();

            // 5. Return result
            var result = _mapper.Map<List<BookDTO_ToReturn>>(books);

            return ResponseResultWithPagination.Success(result, paginationResult);

        }
    }
}