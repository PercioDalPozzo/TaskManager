using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FluentAssertions;
using Moq;
using System.ComponentModel;

namespace Domain.Tests.Services
{
    public class AuthenticationServiceTests
    {
        [Fact(DisplayName = "GIVEN some login WHEN user not exists THEN must return invalid")]
        [Category("Services")]
        public void AuthenticationService_Invalid_User()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var service = new AuthenticationService(userRepositoryMock.Object);

            //Action
            var response = service.Valid("someLogin", "somePassword");

            //Assert
            response.Valid.Should().BeFalse();
        }

        [Fact(DisplayName = "GIVEN some login WHEN invalid password THEN must return invalid")]
        [Category("Services")]
        public void AuthenticationService_Invalid_Login()
        {
            //Arrange
            var login = "Pércio";

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(p => p.GetByLogin(login)).Returns(new User(login, "123456"));

            var service = new AuthenticationService(userRepositoryMock.Object);

            //Action
            var response = service.Valid(login, "somePassword");

            //Assert
            response.Valid.Should().BeFalse();
        }

        [Fact(DisplayName = "GIVEN some login WHEN is valid login THEN must return ok")]
        [Category("Services")]
        public void AuthenticationService_Valid_Login()
        {
            //Arrange
            var login = "Pércio";

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(p => p.GetByLogin(login)).Returns(new User(login, "E10ADC3949BA59ABBE56E057F20F883E"));

            var service = new AuthenticationService(userRepositoryMock.Object);

            //Action
            var response = service.Valid(login, "123456");

            //Assert
            response.Valid.Should().BeTrue();
            response.UserId.Should().NotBeNull();
        }

        [Fact(DisplayName = "GIVEN some value WHEN encrypt THEN must return MD5")]
        [Category("Services")]
        public void AuthenticationService_Encrypt()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var service = new AuthenticationService(userRepositoryMock.Object);

            //Action
            var response = service.Encrypt("123456");

            //Assert
            response.Should().Be("E10ADC3949BA59ABBE56E057F20F883E");
        }
    }
}