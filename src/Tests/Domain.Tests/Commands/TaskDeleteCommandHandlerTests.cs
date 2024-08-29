using Domain.Commands.TaskDelete;
using Domain.Interfaces;
using Domain.Tests.Fakers;
using Moq;
using System.ComponentModel;
using Task = Domain.Entities.Task;

namespace Domain.Tests.Entities
{
    public class TaskDeleteCommandHandlerTests
    {
        [Fact(DisplayName = "GIVEN some task WHEN delete THEN should not call delete of repository")]
        [Category("Handler")]
        public void TaskDeleteCommandHandler_Handler_WithouTask()
        {
            //Arrange
            var command = new TaskDeleteCommand(Guid.NewGuid());

            var taskRepositoryMock = new Mock<ITaskRepository>();

            var handler = new TaskDeleteCommandHandler(taskRepositoryMock.Object);

            //Action
            handler.Handle(command);

            //Assert
            taskRepositoryMock.Verify(p => p.Delete(It.IsAny<Task>()), Times.Never());
        }


        [Fact(DisplayName = "GIVEN some task WHEN delete THEN must call delete of repository")]
        [Category("Handler")]
        public void TaskDeleteCommandHandler_Handler()
        {
            //Arrange
            var command = new TaskDeleteCommand(Guid.NewGuid());

            var taskRepositoryMock = new Mock<ITaskRepository>();
            taskRepositoryMock.Setup(p => p.GetById(command.Id)).Returns(TaskFaker.Build().Generate());

            var handler = new TaskDeleteCommandHandler(taskRepositoryMock.Object);

            //Action
            handler.Handle(command);

            //Assert
            taskRepositoryMock.Verify(p => p.Delete(It.IsAny<Task>()), Times.Once());
        }
    }
}