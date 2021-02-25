using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Domain.Models;
using UserMicroservice.EntityFramework;

namespace UserMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContextFactory _contextFactory;

        public UsersController(UserDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/email/email/
        [HttpGet("email/{email}/")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var context = _contextFactory.CreateDbContext();
            context.Entry(user).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var context = _contextFactory.CreateDbContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            return context.Users.Any(e => e.Id == id);
        }

        // DELETE: api/Users/email/email
        [HttpDelete("email/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
