using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using UserMicroservice.EntityFramework;
using UserMicroservice.API.Controllers;
using System.Linq;
using UserMicroservice.Domain.Models;
using System.Collections.Generic;
using Bogus;
using static Bogus.DataSets.Name;

namespace UserMicroservice.Tests
{
    public class UsersControllerTests
    {
        public Faker<User> Faker { get;set;}

        public UsersControllerTests()
        {
            Faker = new Faker<User>()
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName(Gender.Male))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Adress, f => f.Address.StreetAddress())
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Phone2, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Id, f => f.Random.Guid());
        }

        [Fact]
        public async Task GetAll_ReturnUsers()
        {
            Action<DbContextOptionsBuilder> builder = o => o.UseInMemoryDatabase("entre2ages");
            var factory = new UserDbContextFactory(builder);
            
            var context = factory.CreateDbContext();
            
            var listUsers = Enumerable.Range(1, 10)
              .Select(_ => Faker.Generate())
              .ToList();

            context.Users.AddRange(listUsers);
            context.SaveChanges();

            var controller = new UsersController(factory);

            var result = await controller.GetUsers();

            var model = Assert.IsAssignableFrom<IEnumerable<User>>(result.Value);
            Assert.Equal(10, model.Count());
        }

        [Fact]
        public async Task GetUserById_ReturnUser()
        {
            Action<DbContextOptionsBuilder> builder = o => o.UseInMemoryDatabase("entre2ages");
            var factory = new UserDbContextFactory(builder);

            var context = factory.CreateDbContext();

            var user = Faker.Generate();

            context.Users.Add(user);

            context.SaveChanges();

            var controller = new UsersController(factory);

            var result = await controller.GetUser(user.Id);
            var model = Assert.IsAssignableFrom<User>(result.Value);
            Assert.Equal(user.Id, model.Id);
            Assert.Equal(user.Adress, model.Adress);
        }
    }
}
