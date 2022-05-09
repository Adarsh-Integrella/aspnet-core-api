using aspnetApi.Authentication;
using aspnetApi.Models.ViewModels;
using aspnetApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorsService.AddAuthor(author);
            return Ok();
        }
        [HttpGet("get-author-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author =_authorsService.GetAuthorbyId(id);
            return Ok(author);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("Delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            _authorsService.DeleteAuthorById(id);
            return Ok();
        }

        [HttpGet("get-author-with-book-by-id/{id}")]
        public IActionResult GetAuthorWithBook(int id)
        {
            var response = _authorsService.GetAuthorWithBook(id);
            return Ok(response);

        }
    }
}