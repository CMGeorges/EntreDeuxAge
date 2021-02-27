using System;
using System.Threading.Tasks;
using Bogus;
using EventMicroservice.Domain.Models;
using EventMicroservice.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventMicroservice.Tests
{
    public class EventsControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnEvents()
        {
            Action<DbContextOptionsBuilder> builder = o => o.UseInMemoryDatabase("entre2ages");
            var factory = new EventDbContextFactory(builder);

            var context = factory.CreateDbContext();


        }
    }
}
