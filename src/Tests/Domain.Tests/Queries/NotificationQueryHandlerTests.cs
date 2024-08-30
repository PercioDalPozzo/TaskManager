using Domain.Commands.NotificationQuery;
using Domain.Interfaces;
using Domain.Tests.Fakers;
using FluentAssertions;
using Moq;
using System.ComponentModel;

namespace Domain.Tests.Entities
{
    public class NotificationQueryHandlerTests
    {
        [Fact(DisplayName = "GIVEN some user WHEN get notifications THEN return records")]
        [Category("Query")]
        public async Task NotificationQueryHandler_Handler()
        {
            //Arrange
            var responseMocked = NotificationFaker.Build().Generate(2);

            var userId = Guid.NewGuid();
            var taskRepositoryMock = new Mock<INotificationRepository>();
            taskRepositoryMock.Setup(p => p.GetNotReadByUserId(userId)).ReturnsAsync(responseMocked);

            var query = new NotificationQueryHandler(taskRepositoryMock.Object);

            //Action
            var response = await query.Handle(new NotificationQuery(userId));

            //Assert
            responseMocked.Count.Should().Be(response.Records.Count());

            taskRepositoryMock.Verify(p => p.GetNotReadByUserId(userId), Times.Once());
        }
    }
}