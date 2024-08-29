using Domain.Commands.UserRegister;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System.ComponentModel;

namespace Domain.Tests.Entities
{
    public class UserRegisterCommandHandlerTests
    {
        [Fact(DisplayName = "GIVEN user WHEN register THEN must add with encrypted password")]
        [Category("Handler")]
        public void UserRegisterCommandHandler_Handler()
        {
            //Arrange
            var command = new UserRegisterCommand()
            {
                Login = "Pércio",
                Password = "123456"
            };


            var userRepositoryMock = new Mock<IUserRepository>();

            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(p => p.Encrypt(command.Password)).Returns("ABC");

            var handler = new UserRegisterCommandHandler(userRepositoryMock.Object, authenticationServiceMock.Object);

            //Action
            var response = handler.Handle(command);

            //Assert
            Assert.NotEqual(Guid.Empty, response);
            userRepositoryMock.Verify(p => p.Add(It.IsAny<User>()), Times.Once());
            authenticationServiceMock.Verify(p => p.Encrypt(It.IsAny<string>()), Times.Once());
        }
    }
}