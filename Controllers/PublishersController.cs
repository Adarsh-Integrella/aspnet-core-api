using System;
using aspnetApi.Authentication;
using aspnetApi.Models.ViewModels;
using aspnetApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }
        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublisher(string sortBy, string searchString)
        {
            try
            {
                var _result = _publishersService.GetAllPublishers(sortBy, searchString);
                return Ok(_result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, we could not load the publishers.");
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publishersService.GetPublisherById(id);
            if (_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-publisher-book-with-author/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            try
            {
                var _response = _publishersService.GetPublisherData(id);
                return Ok(_response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}