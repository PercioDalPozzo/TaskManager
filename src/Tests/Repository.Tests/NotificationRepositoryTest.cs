using Domain.Tests.Fakers;
using Repository.Tests.Fakers;
using System.ComponentModel;

namespace Repository.Tests
{
    public class NotificationRepositoryTest
    {
        [Fact(DisplayName = "GIVEN this repositoty WHEN GetByLogin THEN must find record")]
        [Category("Repository")]
        public void Repository_GetById()
        {
            // Arrange
            var context = ContextFake.Build();
            var any = context.Notification.FirstOrDefault();

            var repository = new NotificationRepository(context);

            // Action
            var response = repository.GetById(any.Id);

            // Assert
            Assert.Equal(any.Id, response.Id);
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN GetNotReadByUserId THEN must return records")]
        [Category("Repository")]
        public void Repository_GetNotReadByUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var context = ContextFake.Build();
            context.Notification.AddRange(NotificationFaker.Build()
                .RuleFor(p => p.UserId, userId)
                .Generate(5));
            context.SaveChanges();

            var repository = new NotificationRepository(context);

            // Action
            var response = repository.GetNotReadByUserId(userId);

            // Assert
            Assert.Equal(5, response.Count());
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Add THEN must add in context")]
        [Category("Repository")]
        public void Repository_Add()
        {
            // Arrange
            var context = ContextFake.Build();
            var count = context.Notification.Count();

            var repository = new NotificationRepository(context);

            // Action
            repository.Add(NotificationFaker.Build().Generate());

            // Assert
            Assert.Equal(count + 1, context.Notification.Count());
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Update THEN must update in context")]
        [Category("Repository")]
        public void Repository_Update()
        {
            // Arrange
            var context = ContextFake.Build();
            var any = context.Notification.FirstOrDefault();
            any.ToRead();

            var repository = new NotificationRepository(context);

            // Action
            repository.Update(any);

            // Assert
            Assert.True(context.Notification.Any(p => p.Read));
        }
    }
}