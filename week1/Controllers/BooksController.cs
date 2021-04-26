using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using week1.DTOs;
using week1.Models;
using week1.Services.Book;

namespace week1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get(string name)
        {
            var result = "Hello" + name;
            return Ok(result);
        }

        [HttpGet("News")]
        public IActionResult GetNews(string name)
        {
            var result = "Hello" + name;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDTO_ToCreate input)
        {
            var result = await _bookService.Create(input);
            if (result.IsSuccess)
            {
                return Ok(result);
            }else {
                throw new Exception(result.Message);
            }
        }

        [HttpGet("Now")]
        [Authorize(Roles="Manager")]
        public IActionResult GetNow()
        {
            var result = DateTime.Now;
            return Ok(result);
        }

        [HttpPost("PostFromModel")]
        public IActionResult PostFromModel(Book input)
        {
            var result = input;
            return Ok(result);
        }

        [HttpGet("GetBooks")]
        public IActionResult GetBooks()
        {
            var bookList = new List<Book>();

            // Short hand
            bookList.Add(new Book() { Id = 1, Name = "Salmon", Price = 20, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 2, Name = "Tomato", Price = 23, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 3, Name = "FrenchFries", Price = 42, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 4, Name = "Burger", Price = 65, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 5, Name = "Cola", Price = 34, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 6, Name = "Salad", Price = 23, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 7, Name = "Cheese", Price = 24, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 8, Name = "Mango", Price = 53, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 9, Name = "Nuts", Price = 24, CreatedDate = DateTime.Now });

            //Add
            var baconBook = new Book() { Id = 10, Name = "Bacon", Price = 30, CreatedDate = DateTime.Now };
            bookList.Add(baconBook);

            return Ok(bookList);
        }

        [HttpGet("SearchBooks")]
        public IActionResult SearchBooks(string searchText)
        {
            var bookList = new List<Book>();

            // Short hand
            bookList.Add(new Book() { Id = 1, Name = "Salmon", Price = 20, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 2, Name = "Tomato", Price = 23, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 3, Name = "FrenchFries", Price = 42, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 4, Name = "Burger", Price = 65, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 5, Name = "Cola", Price = 34, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 6, Name = "Salad", Price = 23, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 7, Name = "Cheese", Price = 24, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 8, Name = "Mango", Price = 53, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 9, Name = "Nuts", Price = 24, CreatedDate = DateTime.Now });
            bookList.Add(new Book() { Id = 10, Name = "Bacon", Price = 30, CreatedDate = DateTime.Now });

            var searchResult = bookList.Where(x => x.Name.Contains(searchText)).ToList();
            // var searchResult = bookList.Where(x => x.Name.Contains(searchText)).First();


            return Ok(searchResult);
        }

         [HttpPost("SearchBooksPaginate")]
        public async Task<IActionResult> SearchBooksPaginate( BookDTO_Filter filter)
        {
            var result = await _bookService.SearchPaginate(filter);
            return Ok(result);
        }

        // [HttpGet("SearchBooksPaginate")]
        // public async Task<IActionResult> SearchBooksPaginate([FromQuery] BookDTO_Filter filter)
        // {
        //     var result = await _bookService.SearchPaginate(filter);
        //     return Ok(result);
        // }

    }
}