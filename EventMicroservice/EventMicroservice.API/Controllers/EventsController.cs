using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventMicroservice.Domain.Models;
using EventMicroservice.EntityFramework;

namespace EventMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventDbContextFactory _contextFactory;

        public EventsController(EventDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Events.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            var @event = await context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }
            var context = _contextFactory.CreateDbContext();

            context.Entry(@event).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            var context = _contextFactory.CreateDbContext();
            context.Events.Add(@event);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            var @event = await context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            context.Events.Remove(@event);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            return context.Events.Any(e => e.Id == id);
        }
    }
}
