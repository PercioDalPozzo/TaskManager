using Domain.Tests.Fakers;
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
            var context = ContextFake.Build();
            var any = context.User.FirstOrDefault();

            var repository = new UserRepository(context);

            // Action
            var response = repository.GetByLogin(any.Login);

            // Assert
            Assert.Equal(any.Login, response.Login);
        }

        [Fact(DisplayName = "GIVEN this repositoty WHEN Add THEN must add in context")]
        [Category("Repository")]
        public void Repository_Add()
        {
            // Arrange
            var context = ContextFake.Build();
            var count = context.User.Count();

            var repository = new UserRepository(context);

            // Action
            repository.Add(UserFaker.Build().Generate());

            // Assert
            Assert.Equal(count + 1, context.User.Count());
        }
    }
}