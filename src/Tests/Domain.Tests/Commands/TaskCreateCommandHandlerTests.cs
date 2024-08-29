using Domain.Commands.TaskCreate;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System.ComponentModel;
using Task = Domain.Entities.Task;

namespace Domain.Tests.Entities
{
    public class TaskCreateCommandHandlerTests
    {
        [Fact(DisplayName = "GIVEN some task WHEN create THEN must add new task and new notification")]
        [Category("Handler")]
        public void TaskCreateCommandHandler_Handler()
        {
            //Arrange
            var command = new TaskCreateCommand()
            {
                UserId = Guid.NewGuid(),
                Title = "",
                Description = "",
                LimitToComplete = DateTime.Today.AddDays(2)
            };

            var taskRepositoryMock = new Mock<ITaskRepository>();
            var notificationRepositoryMock = new Mock<INotificationRepository>();

            var handler = new TaskCreateCommandHandler(taskRepositoryMock.Object, notificationRepositoryMock.Object);

            //Action
            var response = handler.Handle(command);


            //Assert
            taskRepositoryMock.Verify(p => p.Add(It.Is<Task>(p => p.UserId == command.UserId
                                                               && p.Title == command.Title
                                                               && p.Description == command.Description
                                                               && p.LimitToComplete == command.LimitToComplete)), Times.Once());


            notificationRepositoryMock.Verify(p => p.Add(It.Is<Notification>(p => p.UserId == command.UserId
                                                                               && p.TaskId == response
                                                                               && p.Message == "Nova tarefa criada")), Times.Once());
        }
    }
}