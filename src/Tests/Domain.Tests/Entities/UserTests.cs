using Domain.Entities;
using FluentAssertions;
using System.ComponentModel;

namespace Domain.Tests.Entities
{
    public class UserTests
    {
        [Fact(DisplayName = "GIVEN some user WHEN new instance THEN set some properties")]
        [Category("Entity")]
        public void User_NewInstance()
        {
            //Arrange
            var record = new User("Pércio", "123456");

            //Action

            //Assert
            record.Id.Should().NotBe(Guid.Empty);
            record.Login.Should().Be("Pércio");
            record.Password.Should().Be("123456");
        }
    }
}