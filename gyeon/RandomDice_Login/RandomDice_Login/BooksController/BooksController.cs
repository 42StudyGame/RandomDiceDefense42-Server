using RandomDice_Login.Models;
using RandomDice_Login.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService_Maira _bookService;

        public BooksController(BookService_Maira bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public ActionResult<Book_Maria> Get(string id)
        {
            var book = _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        
        }

        [HttpPost]
        public JsonResult Create([FromBody]Book_Maria book)
        {
            if (_bookService.InsertBook(book) == false)
            {
                return new JsonResult("실패");
            }

            return new JsonResult("성공");
        }

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Book bookIn)
        //{
        //    var book = _bookService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _bookService.Update(id, bookIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var book = _bookService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _bookService.Remove(id);

        //    return NoContent();
        //}
    }
}