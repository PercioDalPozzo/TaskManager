using Api.Controllers;
using Api.Controllers.Dto;
using Api.Dtos;
using Domain.Commands.UserRegister;
using Domain.Interfaces;
using Domain.Services.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;

namespace Api.Tests
{
    public class UsersControllerTest
    {
        private readonly Mock<ICommandResultHandler<UserRegisterCommand, Guid>> handler;
        private readonly Mock<IAuthenticationService> authService;


        public UsersControllerTest()
        {
            handler = new Mock<ICommandResultHandler<UserRegisterCommand, Guid>>();
            authService = new Mock<IAuthenticationService>();

        }

        [Fact(DisplayName = "GIVEN some request WHEN Register THEN must call handler")]
        [Category("Controller")]
        public void UsersController_GetByUserId()
        {
            // Arrange
            var controller = new UsersController(handler.Object, authService.Object);
            var command = new UserRegisterCommand();
            var userId = Guid.NewGuid();

            handler.Setup(p => p.Handle(It.IsAny<UserRegisterCommand>())).Returns(userId);

            // Action
            var response = controller.Register(command);

            //Assert
            var responseStatus = (OkObjectResult)response;

            handler.Verify(p => p.Handle(It.IsAny<UserRegisterCommand>()), Times.Once());

            response.Should().NotBeNull();
            responseStatus.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact(DisplayName = "GIVEN some request WHEN Login is invalid THEN must return unauthorized")]
        [Category("Controller")]
        public void UsersController_Login_Unauthorized()
        {
            // Arrange
            var dto = new UserLoginDto()
            {
                Username = "Percio",
                Password = "123"
            };

            authService.Setup(p => p.Valid(dto.Username, dto.Password)).Returns(new AuthenticationResponse(false, ""));

            var controller = new UsersController(handler.Object, authService.Object);

            // Action
            var response = controller.Login(dto);

            //Assert
            var responseStatus = (UnauthorizedResult)response;

            authService.Verify(p => p.Valid(dto.Username, dto.Password), Times.Once());

            response.Should().NotBeNull();
            responseStatus.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact(DisplayName = "GIVEN some request WHEN Login is invalid THEN must return token")]
        [Category("Controller")]
        public void UsersController_Login_Token()
        {
            // Arrange
            var dto = new UserLoginDto()
            {
                Username = "Percio",
                Password = "123"
            };

            authService.Setup(p => p.Valid(dto.Username, dto.Password)).Returns(new AuthenticationResponse(true, ""));

            var controller = new UsersController(handler.Object, authService.Object);

            // Action
            var response = controller.Login(dto);

            //Assert
            var responseStatus = (OkObjectResult)response;
            var loginResponse = (LoginResponse)responseStatus.Value;

            authService.Verify(p => p.Valid(dto.Username, dto.Password), Times.Once());

            response.Should().NotBeNull();
            responseStatus.StatusCode.Should().Be(StatusCodes.Status200OK);
            loginResponse.Token.Split('.').Should().HaveCount(3);
        }
    }
}
