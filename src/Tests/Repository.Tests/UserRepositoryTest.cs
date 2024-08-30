using Domain.Tests.Fakers;
using FluentAssertions;
using Repository.Tests.Fakers;
using System.ComponentModel;

namespace Repository.Tests
{
    public class UserRepositoryTest
    {
        [Fact(DisplayName = "GIVEN this repositoty WHEN GetByLogin THEN must find record")]
        [Category("Repository")]
        public void Repository_GetByLogin()
        {
            // Arrange
            var context = new ContextFake().Build();
            var any = context.User.First();

            var repository = new UserRepository(context);

            // Action
            var response = repository.GetByLogin(any.Login);

            // Assert
            response?.Login.Should().Be(any.Login);
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Add THEN must add in context")]
        [Category("Repository")]
        public void Repository_Add()
        {
            // Arrange
            var context = new ContextFake().Build();
            var count = context.User.Count();

            var repository = new UserRepository(context);

            // Action
            repository.Add(UserFaker.Build().Generate());

            // Assert
            context.User.Should().HaveCount(count + 1);
        }
    }
}