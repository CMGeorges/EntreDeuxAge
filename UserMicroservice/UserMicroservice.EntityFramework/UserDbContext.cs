using Microsoft.EntityFrameworkCore;
using UserMicroservice.Domain.Models;

namespace UserMicroservice.EntityFramework
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public UserDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}