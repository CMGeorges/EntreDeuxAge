using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventMicroservice.EntityFramework
{
    public class EventDbContextFactory : IDesignTimeDbContextFactory<EventDbContext>
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public EventDbContextFactory()
        {

        }

        public EventDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            this._configureDbContext = configureDbContext;
        }

        public EventDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<EventDbContext>();

            _configureDbContext(options);

            return new EventDbContext(options.Options);
        }
    }
}
