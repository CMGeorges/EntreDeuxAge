using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserMicroservice.EntityFramework
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public UserDbContextFactory()
        {
            var connexionString = Environment.GetEnvironmentVariable("USERS_CONNECTION_STRING") ??
                                   "Server=localhost;Username=postgres;Password=changeme;Database=entre2ages;Port=5432";
            _configureDbContext = o => o.UseNpgsql(connexionString).UseSnakeCaseNamingConvention();
        }

        public UserDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public UserDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<UserDbContext>();
            _configureDbContext(options);

            return new UserDbContext(options.Options);
        }
    }
}