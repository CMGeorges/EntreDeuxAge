using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/email/
        [HttpGet("{email}/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, User update)
        {
            if (id != update.Id)
            {
                return BadRequest();
            }

            var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = update.Name;
            user.Password = update.Password;
            user.Phone = update.Phone;
            user.Phone2 = update.Phone2;
            user.Adress = update.Adress;
            user.City = update.City;
            user.ZipCode = update.ZipCode;
            user.Email = update.Email;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Post(User postUser)
        {
            var user = new User()
            {
                Name = postUser.Name,
                Password = postUser.Password,
                Phone = postUser.Phone,
                Phone2 = postUser.Phone2,
                Adress = postUser.Adress,
                City = postUser.City,
                ZipCode = postUser.ZipCode,
                Email = postUser.Email
            };
            var context = _contextFactory.CreateDbContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteById(Guid id)
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

        // DELETE: api/Users/email
        [HttpDelete("{email}/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteByEmail(string email)
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

        private bool UserExists(Guid id)
        {
            var context = _contextFactory.CreateDbContext();
            return context.Users.Any(e => e.Id == id);
        }
    }
}
