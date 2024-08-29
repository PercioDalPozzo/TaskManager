using Domain.Commands.NotificationQuery;
using Domain.Interfaces;
using Moq;
using System.ComponentModel;
using Notification = Domain.Entities.Notification;
using Bogus;
using Domain.Tests.Fakers;

namespace Domain.Tests.Entities
{
    public class NotificationQueryHandlerTests
    {
        [Fact(DisplayName = "GIVEN some user WHEN get notifications THEN return records")]
        [Category("Query")]
        public void NotificationQueryHandler_Handler()
        {
            //Arrange
            var responseMocked = NotificationFaker.Build().Generate(2);

            var userId = Guid.NewGuid();
            var taskRepositoryMock = new Mock<INotificationRepository>();
            taskRepositoryMock.Setup(p => p.GetNotReadByUserId(userId)).Returns(responseMocked);

            var query = new NotificationQueryHandler(taskRepositoryMock.Object);

            //Action
            var response = query.Handle(new NotificationQuery(userId));

            //Assert
            Assert.Equal(responseMocked.Count, response.Records.Count());

            taskRepositoryMock.Verify(p => p.GetNotReadByUserId(userId), Times.Once());
        }
    }
}