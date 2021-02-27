using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageMicroservice.Domain.Models;
using MessageMicroservice.Services.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageMicroservice.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _service;

        public MessagesController(MessageService service)
        {
            _service = service;
        }

        // GET: api/<MessagesController>
        [HttpGet]
        public async Task<List<Message>> GetAsync()
        {
            return await _service.GetAsync();
        }

        // GET api/<MessagesController>/5
        [HttpGet("{id}")]
        public async Task<Message> GetAsync(string id)
        {
            return await _service.GetByIdAsync(id);
        }

        // GET api/<MessagesController>/5
        [HttpGet("{guid:Guid}")]
        public async Task<List<Message>> GetByGuidAsync(Guid guid)
        {
            return await _service.GetByGuidAsync(guid);
        }

        // POST api/<MessagesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            await _service.Insert(message);
            return Accepted();
        }

        // PUT/PATCH api/<MessagesController>/5
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Message message)
        {
            var oldMessage = await _service.GetByIdAsync(id);
            if(oldMessage == null)
            {
                return NotFound(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newMessage = await _service.UpdateByIdAsync(id, message);
            return Accepted(newMessage);
        }

        // DELETE api/<MessagesController>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {      
            var result = await _service.DeleteAll();
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<MessagesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteById(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
