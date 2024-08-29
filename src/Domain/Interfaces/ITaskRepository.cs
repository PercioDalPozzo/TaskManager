namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<Entities.Task> GetAllByUserId(Guid userId);
        IEnumerable<Entities.Task> GetOpen(DateTime limitToComplete);
        void Add(Entities.Task task);
        void Delete(Entities.Task task);
        Entities.Task GetById(Guid id);
        void Update(Entities.Task task);
    }
}
