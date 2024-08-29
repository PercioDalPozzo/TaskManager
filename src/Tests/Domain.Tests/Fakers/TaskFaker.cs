using Bogus;
using Task = Domain.Entities.Task;

namespace Domain.Tests.Fakers
{
    public static class TaskFaker
    {
        public static Faker<Task> Build()
        {
            return new Faker<Task>()
                .CustomInstantiator(f => new Task(f.Random.Guid(), f.Random.Word(), f.Random.Words(), f.Date.Soon()));
        }
    }
}
