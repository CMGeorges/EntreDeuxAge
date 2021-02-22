using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserMicroservice.EntityFramework
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        private readonly string _connexionString;

        public UserDbContextFactory()
        {
            
        }

        public UserDbContextFactory(string connexionString)
        {
            _connexionString = connexionString;
        }

        public UserDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<UserDbContext>();
            //var cs = "Host=localhost;Username=postgres;Password=pass;Database=entre2ages";
            
            options.UseNpgsql(_connexionString);
            //options.UseNpgsql(cs);
            return new UserDbContext(options.Options);
        }
    }
}