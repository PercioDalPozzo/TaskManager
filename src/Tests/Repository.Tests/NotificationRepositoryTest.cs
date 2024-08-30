using Domain.Tests.Fakers;
using FluentAssertions;
using Repository.Tests.Fakers;
using System.ComponentModel;

namespace Repository.Tests
{
    public class NotificationRepositoryTest
    {
        [Fact(DisplayName = "GIVEN this repositoty WHEN GetById THEN must find record")]
        [Category("Repository")]
        public void Repository_GetById()
        {
            // Arrange
            var context = new ContextFake().Build();
            var any = context.Notification.First();

            var repository = new NotificationRepository(context);

            // Action
            var response = repository.GetById(any.Id);

            // Assert
            response?.Id.Should().Be(any.Id);
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN GetNotReadByUserId THEN must return records")]
        [Category("Repository")]
        public async Task Repository_GetNotReadByUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var context = new ContextFake().Build();
            context.Notification.AddRange(NotificationFaker.Build()
                .RuleFor(p => p.UserId, userId)
                .Generate(5));
            context.SaveChanges();

            var repository = new NotificationRepository(context);

            // Action
            var response = await repository.GetNotReadByUserId(userId);

            // Assert
            response.Should().HaveCount(5);
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Add THEN must add in context")]
        [Category("Repository")]
        public void Repository_Add()
        {
            // Arrange
            var context = new ContextFake().Build();
            var count = context.Notification.Count();

            var repository = new NotificationRepository(context);

            // Action
            repository.Add(NotificationFaker.Build().Generate());

            // Assert
            context.Notification.Should().HaveCount(count + 1);
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Update THEN must update in context")]
        [Category("Repository")]
        public void Repository_Update()
        {
            // Arrange
            var context = new ContextFake().Build();
            var any = context.Notification.First();
            any.ToRead();

            var repository = new NotificationRepository(context);

            // Action
            repository.Update(any);

            // Assert
            context.Notification.Should().Contain(p => p.Read);
        }
    }
}