using Domain.Commands.TaskQuery;
using Domain.Interfaces;
using Moq;
using System.ComponentModel;
using Task = Domain.Entities.Task;
using Bogus;
using Domain.Tests.Fakers;

namespace Domain.Tests.Entities
{
    public class TaskQueryHandlerTests
    {
        [Fact(DisplayName = "GIVEN some user WHEN get tasks THEN return records")]
        [Category("Query")]
        public void TaskQueryHandler_Handler()
        {
            //Arrange
            var responseMocked = TaskFaker.Build().Generate(2);

            var userId = Guid.NewGuid();
            var taskRepositoryMock = new Mock<ITaskRepository>();
            taskRepositoryMock.Setup(p => p.GetAllByUserId(userId)).Returns(responseMocked);

            var query = new TaskQueryHandler(taskRepositoryMock.Object);

            //Action
            var response = query.Handle(new TaskQuery(userId));

            //Assert
            Assert.Equal(responseMocked.Count, response.Records.Count());

            taskRepositoryMock.Verify(p => p.GetAllByUserId(userId), Times.Once());
        }
    }
}