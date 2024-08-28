using Domain.Entity;
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
            Assert.NotEqual(Guid.Empty, record.Id);
            Assert.NotEqual(Guid.Empty, record.UserId);
            Assert.NotEqual(Guid.Empty, record.TaskId);
            Assert.False(record.Read);
            Assert.Equal(DateTime.Today, record.Created.Date);
            Assert.Equal("Any message", record.Message);
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
            Assert.True(record.Read);
            Assert.Equal(DateTime.Today, record.ReadDate?.Date);
        }
    }
}