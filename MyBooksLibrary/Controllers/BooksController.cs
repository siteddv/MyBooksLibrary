using Microsoft.AspNetCore.Mvc;
using MyBooksLibrary.Data.Services;
using MyBooksLibrary.Data.ViewModels;

namespace MyBooksLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookService _booksService;

        public BooksController(BookService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var allBooks = _booksService.GetBookById(id);
            return Ok(allBooks);
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody]BookViewModel book)
        {
            _booksService.AddBook(book);
            return Ok();
        }

    }
}
