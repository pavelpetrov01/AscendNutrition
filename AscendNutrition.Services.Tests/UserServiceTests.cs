using AscendNutrition.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Models;

namespace AscendNutrition.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAllUsersWithRoles()
        {
            var user1 = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                Email = "john.doe@example.com"
            };

            var user2 = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                Email = "jane.doe@example.com"
            };

            var users = new List<ApplicationUser> { user1, user2 }.AsQueryable();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<ApplicationUser>>(),
                new List<IUserValidator<ApplicationUser>>(),
                new List<IPasswordValidator<ApplicationUser>>(),
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                Mock.Of<ILogger<UserManager<ApplicationUser>>>()
            );

            mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync((ApplicationUser user) => new List<string> { "User" });

            mockUserManager.Setup(um => um.Users)
                .Returns(users.BuildMock()); 

           
            var service = new UserService(mockUserManager.Object);

            var result = await service.GetAllUsersAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("john.doe@example.com", result.First().Email);
            Assert.AreEqual("User", result.First().Roles.First());
            Assert.AreEqual("jane.doe@example.com", result.Last().Email);
            Assert.AreEqual("User", result.Last().Roles.First());
        }
    }
}
