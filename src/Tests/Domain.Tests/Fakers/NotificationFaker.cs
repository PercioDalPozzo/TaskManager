using Bogus;
using Domain.Entities;

namespace Domain.Tests.Fakers
{
    public static class NotificationFaker
    {
        public static Faker<Notification> Build()
        {
            return new Faker<Notification>()
                .CustomInstantiator(f => new Notification(f.Random.Guid(), f.Random.Guid(), f.Random.Words()));
        }
    }
}
