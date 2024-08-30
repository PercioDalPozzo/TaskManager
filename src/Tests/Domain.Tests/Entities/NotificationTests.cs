using Domain.Entities;
using FluentAssertions;
using System.ComponentModel;

namespace Domain.Tests.Entities
{
    public class NotificationTests
    {
        [Fact(DisplayName = "GIVEN some notification WHEN new instance THEN set some properties")]
        [Category("Entity")]
        public void Notification_NewInstance()
        {
            //Arrange
            var record = new Notification(Guid.NewGuid(), Guid.NewGuid(), "Any message");

            //Action

            //Assert
            record.Id.Should().NotBe(Guid.Empty);
            record.UserId.Should().NotBe(Guid.Empty);
            record.TaskId.Should().NotBe(Guid.Empty);
            record.Read.Should().BeFalse();
            record.Created.Date.Should().Be(DateTime.Today);
            record.Message.Should().Be("Any message");
        }

        [Fact(DisplayName = "GIVEN a notification WHEN ToRead THEN must set Read and ReadDate")]
        [Category("Entity")]
        public void Notification_ToRead()
        {
            //Arrange
            var record = new Notification(Guid.NewGuid(), Guid.NewGuid(), "Any message");

            //Action
            record.ToRead();

            //Assert
            record.Read.Should().BeTrue();
            record.ReadDate?.Date.Should().Be(DateTime.Today);

        }
    }
}