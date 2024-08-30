using Domain.Commands.TaskQuery;
using Domain.Interfaces;
using Domain.Tests.Fakers;
using FluentAssertions;
using Moq;
using System.ComponentModel;

namespace Domain.Tests.Entities
{
    public class TaskQueryHandlerTests
    {
        [Fact(DisplayName = "GIVEN some user WHEN get tasks THEN return records")]
        [Category("Query")]
        public async Task TaskQueryHandler_Handler()
        {
            //Arrange
            var responseMocked = TaskFaker.Build().Generate(2);

            var userId = Guid.NewGuid();
            var taskRepositoryMock = new Mock<ITaskRepository>();
            taskRepositoryMock.Setup(p => p.GetAllByUserId(userId)).ReturnsAsync(responseMocked);

            var query = new TaskQueryHandler(taskRepositoryMock.Object);

            //Action
            var response = await query.Handle(new TaskQuery(userId));

            //Assert
            responseMocked.Count.Should().Be(response.Records.Count());

            taskRepositoryMock.Verify(p => p.GetAllByUserId(userId), Times.Once());
        }
    }
}