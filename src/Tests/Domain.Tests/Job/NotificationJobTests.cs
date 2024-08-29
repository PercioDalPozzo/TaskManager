using Domain.Commands.UserRegister;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Job;
using Domain.Tests.Fakers;
using Microsoft.Extensions.Logging;
using Moq;
using Quartz;
using System.ComponentModel;

namespace Domain.Tests.Entities
{
    public class NotificationJobTests
    {
        [Fact(DisplayName = "GIVEN user WHEN register THEN must add with encrypted password")]
        [Category("Job")]
        public async void NotificationJob_Execute()
        {
            //Arrange
            var command = new UserRegisterCommand()
            {
                Login = "Pércio",
                Password = "123456"
            };

            var loggerMock = new Mock<ILogger<NotificationJob>>();

            var tasksFake = TaskFaker.Build().Generate(2);
            var taskRepositoryMock = new Mock<ITaskRepository>();
            taskRepositoryMock.Setup(p => p.GetOpen(It.IsAny<DateTime>())).Returns(tasksFake);

            var notificationRepositoryMock = new Mock<INotificationRepository>();

            var job = new NotificationJob(loggerMock.Object, taskRepositoryMock.Object, notificationRepositoryMock.Object);
            var jobExecutionContextMock = new Mock<IJobExecutionContext>();

            //Action
            await job.Execute(jobExecutionContextMock.Object);

            //Assert
            notificationRepositoryMock.Verify(p => p.Add(It.IsAny<Notification>()), Times.Exactly(tasksFake.Count));
        }
    }
}