using Domain.Entity;
using System.ComponentModel;
using Task = Domain.Entity.Task;

namespace Domain.Tests.Entities
{
    public class TaskTests
    {
        [Fact(DisplayName = "GIVEN some task WHEN new instance THEN set some properties")]
        [Category("Entity")]
        public void Task_NewInstance()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var limitToComplete = DateTime.Today.AddDays(3);
            var title = "Title";
            var description = "Description";

            var record = new Task(userId, title, description, limitToComplete);

            //Action

            //Assert
            Assert.NotEqual(Guid.Empty, record.Id);
            Assert.NotEqual(Guid.Empty, record.UserId);
            Assert.False(record.Concluded);
            Assert.Equal(DateTime.Today, record.Created.Date);
            Assert.Equal(title, record.Title);
            Assert.Equal(description, record.Description);
            Assert.Equal(limitToComplete, record.LimitToComplete);
        }

        [Fact(DisplayName = "GIVEN a task WHEN Conclude THEN must set Concluded")]
        [Category("Entity")]
        public void Task_Conclude()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var limitToComplete = DateTime.Today.AddDays(3);
            var title = "Title";
            var description = "Description";

            var record = new Task(userId, title, description, limitToComplete);

            //Action
            record.Conclude();

            //Assert
            Assert.True(record.Concluded);            
        }
    }
}