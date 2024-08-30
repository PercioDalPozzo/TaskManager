using FluentAssertions;
using System.ComponentModel;
using Task = Domain.Entities.Task;

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

            //Action
            var record = new Task(userId, title, description, limitToComplete);

            //Assert
            record.Id.Should().NotBe(Guid.Empty);
            record.UserId.Should().NotBe(Guid.Empty);
            record.Concluded.Should().BeFalse();
            record.Created.Date.Should().Be(DateTime.Today);
            record.Title.Should().Be(title);
            record.Description.Should().Be(description);
            record.LimitToComplete.Should().Be(limitToComplete);
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
            record.Concluded.Should().BeTrue();
        }
    }
}