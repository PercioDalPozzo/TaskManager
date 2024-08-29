using Domain.Commands.NotificationRead;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Tests.Fakers;
using Moq;
using System.ComponentModel;

namespace Domain.Tests.Entities
{
    public class NotificationReadCommandHandlerTests
    {
        [Fact(DisplayName = "GIVEN some notification WHEN read THEN must set to read and update")]
        [Category("Handler")]
        public void NotificationReadCommandHandler_Handler()
        {
            //Arrange
            var command = new NotificationReadCommand(Guid.NewGuid());
            var notificationFake = NotificationFaker.Build().Generate();

            var notificationRepositoryMock = new Mock<INotificationRepository>();
            notificationRepositoryMock.Setup(p => p.GetById(command.Id)).Returns(notificationFake);

            var handler = new NotificationReadCommandHandler(notificationRepositoryMock.Object);

            //Action
            handler.Handle(command);

            //Assert
            notificationRepositoryMock.Verify(p => p.Update(It.Is<Notification>(p => p.Read)), Times.Once());
        }
    }
}