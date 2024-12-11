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
using AscendNutrition.Services.Data.Interfaces;

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

        [Test]
        public async Task DeleteUserAsync_UserNotFound_ReturnsFalse()
        {

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

            var service = new UserService(mockUserManager.Object);
            
            Guid userId = Guid.NewGuid();
            mockUserManager
                .Setup(m => m.FindByIdAsync(userId.ToString()))
                .ReturnsAsync((ApplicationUser?)null);

            bool result = await service.DeleteUserAsync(userId);

            Assert.IsFalse(result);
          
        }

        [Test]
        public async Task DeleteUserAsync_UserFound_DeletionSucceeds_ReturnsTrue()
        {
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

            var service = new UserService(mockUserManager.Object);
           
            Guid userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };

            mockUserManager
                .Setup(m => m.FindByIdAsync(userId.ToString()))
                .ReturnsAsync(user);

            mockUserManager
                .Setup(m => m.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            bool result = await service.DeleteUserAsync(userId);

            Assert.IsTrue(result);
            mockUserManager.Verify(m => m.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_UserFound_DeletionFails_ReturnsFalse()
        {
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

            var service = new UserService(mockUserManager.Object);

            Guid userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId };

            mockUserManager
                .Setup(m => m.FindByIdAsync(userId.ToString()))
                .ReturnsAsync(user);

            mockUserManager
                .Setup(m => m.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Deletion failed." }));

            bool result = await service.DeleteUserAsync(userId);

            Assert.IsFalse(result);
            mockUserManager.Verify(m => m.DeleteAsync(user), Times.Once);
        }
    }
}
