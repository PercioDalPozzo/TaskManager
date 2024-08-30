using Api.Controllers;
using Domain.Commands.TaskConclude;
using Domain.Commands.TaskCreate;
using Domain.Commands.TaskDelete;
using Domain.Commands.TaskQuery;
using Domain.Interfaces;
using Domain.Tests.Fakers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;

namespace Api.Tests
{
    public class TasksControllerTest
    {
        private readonly Mock<ICommandResultHandler<TaskCreateCommand, Guid>> createHandlerMock;
        private readonly Mock<ICommandHandler<TaskDeleteCommand>> deleteHandlerMock;
        private readonly Mock<ICommandHandler<TaskConcludeCommand>> concludeHandlerMock;
        private readonly Mock<IQueryHandler<TaskQuery, TaskQueryResponse>> queryHandlerMock;


        public TasksControllerTest()
        {
            createHandlerMock = new Mock<ICommandResultHandler<TaskCreateCommand, Guid>>();
            deleteHandlerMock = new Mock<ICommandHandler<TaskDeleteCommand>>();
            concludeHandlerMock = new Mock<ICommandHandler<TaskConcludeCommand>>();
            queryHandlerMock = new Mock<IQueryHandler<TaskQuery, TaskQueryResponse>>();
        }

        [Fact(DisplayName = "GIVEN some request WHEN GetAll THEN must return records")]
        [Category("Controller")]
        public void TasksController_GetByUserId()
        {
            // Arrange
            queryHandlerMock.Setup(p => p.Handle(It.IsAny<TaskQuery>()))
                .Returns(new TaskQueryResponse(TaskFaker.Build().Generate(3)));

            var controller = new TasksController(createHandlerMock.Object, deleteHandlerMock.Object, concludeHandlerMock.Object, queryHandlerMock.Object);

            // Action
            var response = controller.GetAll(Guid.NewGuid().ToString());

            //Assert
            var responseStatus = (ObjectResult)response;
            var records = (TaskQueryResponse)responseStatus.Value;

            queryHandlerMock.Verify(p => p.Handle(It.IsAny<TaskQuery>()), Times.Once());

            response.Should().NotBeNull();
            records?.Records.Should().HaveCount(3);
            responseStatus.StatusCode.Should().Be(StatusCodes.Status200OK);
        }


        [Fact(DisplayName = "GIVEN some request WHEN Delete THEN must call handler")]
        [Category("Controller")]
        public void TasksController_Delete()
        {
            //Arrange
            var controller = new TasksController(createHandlerMock.Object, deleteHandlerMock.Object, concludeHandlerMock.Object, queryHandlerMock.Object);

            //Action
            var response = controller.Delete(Guid.NewGuid().ToString());
            var responseStatus = (OkResult)response;

            //Assert
            deleteHandlerMock.Verify(p => p.Handle(It.IsAny<TaskDeleteCommand>()), Times.Once());

            response.Should().NotBeNull();
            responseStatus.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact(DisplayName = "GIVEN some request WHEN Post THEN must call handler")]
        [Category("Controller")]
        public void TasksController_Post()
        {
            //Arrange
            var controller = new TasksController(createHandlerMock.Object, deleteHandlerMock.Object, concludeHandlerMock.Object, queryHandlerMock.Object);

            var command = new TaskCreateCommand();

            //Action
            var response = controller.Post(command);

            //Assert
            var responseStatus = (OkObjectResult)response;

            createHandlerMock.Verify(p => p.Handle(It.IsAny<TaskCreateCommand>()), Times.Once());

            response.Should().NotBeNull();
            responseStatus.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact(DisplayName = "GIVEN some request WHEN Complete THEN must call handler")]
        [Category("Controller")]
        public void TasksController_Complete()
        {
            //Arrange
            var controller = new TasksController(createHandlerMock.Object, deleteHandlerMock.Object, concludeHandlerMock.Object, queryHandlerMock.Object);

            //Action
            var response = controller.Complete(Guid.NewGuid().ToString());

            //Assert
            var responseStatus = (OkResult)response;

            concludeHandlerMock.Verify(p => p.Handle(It.IsAny<TaskConcludeCommand>()), Times.Once());

            response.Should().NotBeNull();
            responseStatus.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
