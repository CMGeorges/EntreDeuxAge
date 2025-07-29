using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using MediaMicroservice.API.Controllers;
using MediaMicroservice.Domain.Models;
using MediaMicroservice.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MediaMicroservice.Tests
{
    public class MediasControllerTests
    {
        public Faker<Media> Faker { get; }
        public MediaDbContextFactory Factory { get; }
        public MediasController Controller { get; }

        public MediasControllerTests()
        {
            Faker = new Faker<Media>()
                .RuleFor(m => m.Id, f => f.Random.Guid())
                .RuleFor(m => m.Title, f => f.Lorem.Sentence())
                .RuleFor(m => m.Data, f => f.Random.Word());

            Action<DbContextOptionsBuilder> builder = o => o.UseInMemoryDatabase("entre2ages");
            Factory = new MediaDbContextFactory(builder);
            Controller = new MediasController(Factory);
        }

        [Fact]
        public async Task GetAll_Returns_Medias()
        {
            var context = Factory.CreateDbContext();
            var medias = Enumerable.Range(0, 5).Select(_ => Faker.Generate()).ToList();
            await context.Medias.AddRangeAsync(medias);
            await context.SaveChangesAsync();

            var result = await Controller.GetAll();

            Assert.Equal(medias.Count, result.Count());
        }
    }
}
