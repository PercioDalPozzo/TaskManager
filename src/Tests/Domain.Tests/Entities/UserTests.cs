using Domain.Entity;
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
            Assert.NotEqual(Guid.Empty, record.Id);
            Assert.Equal("Pércio", record.Login);
            Assert.Equal("123456", record.Password);
        }
    }
}