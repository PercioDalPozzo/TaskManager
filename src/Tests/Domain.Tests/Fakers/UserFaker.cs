using Bogus;
using Domain.Entities;

namespace Domain.Tests.Fakers
{
    public static class UserFaker
    {
        public static Faker<User> Build()
        {
            return new Faker<User>()
                .CustomInstantiator(f => new User(f.Name.FindName(), f.Random.AlphaNumeric(10)));
        }
    }
}
