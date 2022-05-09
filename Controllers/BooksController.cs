using aspnetApi.Authentication;
using aspnetApi.Models.ViewModels;
using aspnetApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks(string sortBy, string searchString)
        {
            var allBooks = _booksService.GetAllBooks(sortBy, searchString);
            return Ok(allBooks);
        }

        [HttpGet("get-book-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("add-book-with-author")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBookWithAuthors(book);
            return Ok();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var updatedBook = _booksService.UpdatebookById(id, book);
            return Ok(updatedBook);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("Delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }
    }
}