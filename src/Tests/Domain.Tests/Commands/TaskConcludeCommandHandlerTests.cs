using Domain.Commands.TaskConclude;
using Domain.Interfaces;
using Domain.Tests.Fakers;
using Moq;
using System.ComponentModel;
using Task = Domain.Entities.Task;

namespace Domain.Tests.Entities
{
    public class TaskConcludeCommandHandlerTests
    {
        [Fact(DisplayName = "GIVEN some task WHEN conclude THEN must change task like concluded")]
        [Category("Handler")]
        public void TaskConcludeCommandHandler_Handler()
        {
            //Arrange
            var command = new TaskConcludeCommand(Guid.NewGuid());

            var taskRepositoryMock = new Mock<ITaskRepository>();
            taskRepositoryMock.Setup(p => p.GetById(command.Id)).Returns(TaskFaker.Build().Generate());

            var handler = new TaskConcludeCommandHandler(taskRepositoryMock.Object);

            //Action
            handler.Handle(command);

            //Assert
            taskRepositoryMock.Verify(p => p.Update(It.Is<Task>(p => p.Concluded)), Times.Once());
        }
    }
}