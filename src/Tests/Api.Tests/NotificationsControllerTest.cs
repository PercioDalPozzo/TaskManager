using Api.Controllers;
using Domain.Commands.NotificationQuery;
using Domain.Commands.NotificationRead;
using Domain.Interfaces;
using Domain.Tests.Fakers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;

namespace Api.Tests
{
    public class NotificationsControllerTest
    {
        private readonly Mock<ICommandHandler<NotificationReadCommand>> notificationReadHandlerMock;
        private readonly Mock<IQueryHandler<NotificationQuery, NotificationQueryResponse>> queryHandlerMock;

        public NotificationsControllerTest()
        {
            notificationReadHandlerMock = new Mock<ICommandHandler<NotificationReadCommand>>();
            queryHandlerMock = new Mock<IQueryHandler<NotificationQuery, NotificationQueryResponse>>();
        }

        [Fact(DisplayName = "GIVEN some request WHEN GetByUserId THEN must return records")]
        [Category("Controller")]
        public void NotificationsController_GetByUserId()
        {
            // Arrange
            queryHandlerMock.Setup(p => p.Handle(It.IsAny<NotificationQuery>()))
                .Returns(new NotificationQueryResponse(NotificationFaker.Build().Generate(3)));

            var controller = new NotificationsController(notificationReadHandlerMock.Object, queryHandlerMock.Object);

            // Action
            var response = controller.GetByUserId(Guid.NewGuid().ToString());

            //Assert
            var responseStatus = (ObjectResult)response;
            var records = (NotificationQueryResponse)responseStatus.Value;

            queryHandlerMock.Verify(p => p.Handle(It.IsAny<NotificationQuery>()), Times.Once());

            Assert.NotNull(response);
            Assert.Equal(3, records?.Records.Count());
            Assert.Equal(StatusCodes.Status200OK, responseStatus.StatusCode);
        }


        [Fact(DisplayName = "GIVEN some request WHEN Read THEN must call handler")]
        [Category("Controller")]
        public void NotificationsController_Read()
        {
            var controller = new NotificationsController(notificationReadHandlerMock.Object, queryHandlerMock.Object);

            var response = controller.Read(Guid.NewGuid().ToString());

            var responseStatus = (OkResult)response;

            //Assert
            notificationReadHandlerMock.Verify(p => p.Handle(It.IsAny<NotificationReadCommand>()), Times.Once());

            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, responseStatus.StatusCode);
        }
    }
}
