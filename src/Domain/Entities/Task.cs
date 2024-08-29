namespace Domain.Entities
{
    public class Task : Entity
    {
        public Task(Guid userId, string title, string description, DateTime limitToComplete)
        {
            UserId = userId;
            Title = title;
            Description = description;
            LimitToComplete = limitToComplete;
        }

        public Guid UserId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime Created { get; private set; } = DateTime.Now;

        public DateTime LimitToComplete { get; private set; }

        public bool Concluded { get; private set; } = false;


        public void Conclude()
        {
            Concluded = true;
        }
    }
}
