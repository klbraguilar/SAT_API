using System;
using System.Collections.Generic;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Core.Entities;
using Sat.Recruitment.Api.Core.Interfaces;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var mockUserValidationService = new Mock<IUserValidationService>();
            mockUserValidationService.Setup(s => s.ValidateErrors(It.IsAny<User>(), It.IsAny<string>()));
            mockUserValidationService.Setup(s => s.IsDuplicateUser(It.IsAny<User>(), It.IsAny<List<User>>())).Returns(false);

            var mockNormalizeEmailService = new Mock<INormalizeEmailService>();
            mockNormalizeEmailService.Setup(s => s.NormalizeEmail(It.IsAny<string>())).Returns("");

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(s => s.getAllUsersFromFile()).Returns(new List<User>());

            var userController = new UsersController(
                mockUserValidationService.Object,
                mockNormalizeEmailService.Object,
                mockUserRepository.Object
            );

            // Act
            var result = userController.CreateUser(new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            }).Result;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            // Arrange
            var mockUserValidationService = new Mock<IUserValidationService>();
            mockUserValidationService.Setup(s => s.ValidateErrors(It.IsAny<User>(), It.IsAny<string>()));
            mockUserValidationService.Setup(s => s.IsDuplicateUser(It.IsAny<User>(), It.IsAny<List<User>>())).Returns(true);

            var mockNormalizeEmailService = new Mock<INormalizeEmailService>();
            mockNormalizeEmailService.Setup(s => s.NormalizeEmail(It.IsAny<string>())).Returns("");

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(s => s.getAllUsersFromFile()).Returns(new List<User>());

            var userController = new UsersController(
                mockUserValidationService.Object,
                mockNormalizeEmailService.Object,
                mockUserRepository.Object
            );

            // Act
            var result = userController.CreateUser(new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            }).Result;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
