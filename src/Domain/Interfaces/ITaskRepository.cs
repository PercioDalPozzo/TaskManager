namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        Entities.Task? GetById(Guid id);
        IEnumerable<Entities.Task> GetAllByUserId(Guid userId);
        IEnumerable<Entities.Task> GetNotConcluded(DateTime limitToComplete);
        void Add(Entities.Task task);
        void Delete(Entities.Task task);
        void Update(Entities.Task task);
    }
}
