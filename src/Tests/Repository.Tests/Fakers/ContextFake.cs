using Domain.Tests.Fakers;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Tests.Fakers
{
    public class ContextFake
    {
        public ApplicationContext Build()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                     .UseInMemoryDatabase(databaseName: $"TestDatabase{Guid.NewGuid()}")
                     .Options;

            var context = new ApplicationContext(options);

            context.Task.AddRange(TaskFaker.Build().Generate(10));
            context.User.AddRange(UserFaker.Build().Generate(10));
            context.Notification.AddRange(NotificationFaker.Build().Generate(10));

            context.SaveChanges();

            return context;
        }

        public ApplicationContext EmptyContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                     .UseInMemoryDatabase(databaseName: $"TestDatabase{Guid.NewGuid()}")
                     .Options;

            return new ApplicationContext(options);
        }
    }

}
