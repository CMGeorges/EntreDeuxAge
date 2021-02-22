using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventMicroservice.EntityFramework
{
    public class EventDbContextFactory : IDesignTimeDbContextFactory<EventDbContext>
    {
        private readonly string _connexionString;

        public EventDbContextFactory()
        {

        }

        public EventDbContextFactory(string connexionString)
        {
            _connexionString = connexionString;
        }

        public EventDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<EventDbContext>();
            var cs = "Host=localhost;Username=postgres;Password=pass;Database=entre2ages;Port=5431";

            options.UseNpgsql(_connexionString);
            //options.UseNpgsql(cs);
            return new EventDbContext(options.Options);
        }
    }
}
