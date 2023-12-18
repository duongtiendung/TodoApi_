using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApiTest
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Register_ValidRequest_ReturnsCreated()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var authController = new AuthController(userServiceMock.Object);

            var loginRequest = new LoginRequest
            {
                // Initialize login request properties
            };

            userServiceMock.Setup(x => x.RegisterUser(It.IsAny<LoginRequest>()))
                           .ReturnsAsync(new User
                           {
                               // Initialize user properties
                           });

            // Act
            var result = await authController.Register(loginRequest);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var user = Assert.IsType<User>(createdResult.Value);
            Assert.Equal("Register", createdResult.ActionName);
            // Add more assertions based on your specific scenario
        }

        [Fact]
        public async Task Login_ValidRequest_ReturnsOk()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var authController = new AuthController(userServiceMock.Object);

            var loginRequest = new LoginRequest
            {
                // Initialize login request properties
            };

            userServiceMock.Setup(x => x.LoginUser(It.IsAny<LoginRequest>()))
                           .ReturnsAsync(new LoginResponse
                           {
                               // Initialize login response properties
                           });

            // Act
            var result = await authController.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<LoginResponse>(okResult.Value);
            // Add more assertions based on your specific scenario
        }

        [Fact]
        public async Task Login_UserNotFound_ReturnsBadRequest()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var authController = new AuthController(userServiceMock.Object);

            var loginRequest = new LoginRequest
            {
                // Initialize login request properties
            };

            userServiceMock.Setup(x => x.LoginUser(It.IsAny<LoginRequest>()))
                           .ThrowsAsync(new Exception("User not found."));

            // Act
            var result = await authController.Login(loginRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("User not found.", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_WrongPassword_ReturnsBadRequest()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var authController = new AuthController(userServiceMock.Object);

            var loginRequest = new LoginRequest
            {
                // Initialize login request properties
            };

            userServiceMock.Setup(x => x.LoginUser(It.IsAny<LoginRequest>()))
                           .ThrowsAsync(new Exception("Wrong password."));

            // Act
            var result = await authController.Login(loginRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Wrong password.", badRequestResult.Value);
        }
    }
}
