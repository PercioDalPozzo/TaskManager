using Domain.Tests.Fakers;
using Repository.Tests.Fakers;
using System.ComponentModel;

namespace Repository.Tests
{
    public class TaskRepositoryTest
    {
        [Fact(DisplayName = "GIVEN this repositoty WHEN GetById THEN must find record")]
        [Category("Repository")]
        public void Repository_GetById()
        {
            // Arrange
            var context = new ContextFake().Build();
            var any = context.Task.First();

            var repository = new TaskRepository(context);

            // Action
            var response = repository.GetById(any.Id);

            // Assert
            Assert.Equal(any.Id, response?.Id);
        }

        [Fact(DisplayName = "GIVEN this repository WHEN GetNotConcluded THEN must return records")]
        [Category("Repository")]

        public void Repository_GetNotReadByUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var context = new ContextFake().EmptyContext();
            context.Task.Add(TaskFaker.Build().RuleFor(p => p.UserId, userId).RuleFor(p => p.LimitToComplete, DateTime.Now).Generate());
            context.Task.Add(TaskFaker.Build().RuleFor(p => p.UserId, userId).RuleFor(p => p.LimitToComplete, DateTime.Now.AddHours(-1)).Generate());
            context.Task.Add(TaskFaker.Build().RuleFor(p => p.UserId, userId).RuleFor(p => p.LimitToComplete, DateTime.Now.AddHours(23)).Generate());
            context.Task.Add(TaskFaker.Build().RuleFor(p => p.UserId, userId).RuleFor(p => p.LimitToComplete, DateTime.Now.AddHours(48)).Generate());
            context.Task.Add(TaskFaker.Build().RuleFor(p => p.UserId, userId).RuleFor(p => p.LimitToComplete, DateTime.Now.AddHours(72)).Generate());
            context.SaveChanges();

            var repository = new TaskRepository(context);

            var tomorrow = DateTime.Now.AddDays(1);

            // Action
            var response = repository.GetNotConcluded(tomorrow);

            // Assert            
            Assert.Equal(3, response.Count());
        }


        [Fact(DisplayName = "GIVEN this repositoty WHEN GetAllByUserId THEN must return records")]
        [Category("Repository")]
        public void Repository_GetAllByUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var context = new ContextFake().Build();
            context.Task.AddRange(TaskFaker.Build().RuleFor(p => p.UserId, userId).RuleFor(p => p.LimitToComplete, DateTime.Today).Generate(3));
            context.SaveChanges();

            var repository = new TaskRepository(context);

            // Action
            var response = repository.GetAllByUserId(userId);

            // Assert
            Assert.Equal(3, response.Count());
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Add THEN must add in context")]
        [Category("Repository")]
        public void Repository_Add()
        {
            // Arrange
            var context = new ContextFake().Build();
            var count = context.Task.Count();

            var repository = new TaskRepository(context);

            // Action
            repository.Add(TaskFaker.Build().Generate());

            // Assert
            Assert.Equal(count + 1, context.Task.Count());
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Update THEN must update in context")]
        [Category("Repository")]
        public void Repository_Update()
        {
            // Arrange
            var context = new ContextFake().Build();
            var any = context.Task.First();
            any.Conclude();

            var repository = new TaskRepository(context);

            // Action
            repository.Update(any);

            // Assert
            Assert.True(context.Task.Any(p => p.Concluded));
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Delete THEN must update in context")]
        [Category("Repository")]
        public void Repository_Delete()
        {
            // Arrange
            var context = new ContextFake().Build();
            var count = context.Task.Count();
            var any = context.Task.First();


            var repository = new TaskRepository(context);

            // Action
            repository.Delete(any);

            // Assert
            Assert.Equal(count - 1, context.Task.Count());
        }
    }
}