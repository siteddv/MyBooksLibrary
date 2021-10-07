using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBooksLibrary.Data.Services;
using MyBooksLibrary.Data.ViewModels;
using MyBooksLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooksLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherViewModel publisher)
        {
            try
            {
                var addedPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), addedPublisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {publisher.Name}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-books-with-authors-by-id/{id}")]
        public IActionResult GetPublisherDataById(int id)
        {
            var response = _publishersService.GetPublisherDataById(id);

            return Ok(response);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var response = _publishersService.GetPublisherDataById(id);

            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
