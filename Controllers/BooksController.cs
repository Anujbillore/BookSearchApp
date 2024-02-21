// Controllers/BooksController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookSearchApp.Models;
using BookSearchApp.Repositories;

namespace BookSearchApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BooksController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SearchBooks(string query)
        {
            var books = await _bookRepository.SearchBooksAsync(query);
            return Ok(books);
        }
    }
}
