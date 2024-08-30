namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        Entities.Task? GetById(Guid id);
        Task<IReadOnlyList<Entities.Task>> GetAllByUserId(Guid userId);
        Task<IReadOnlyList<Entities.Task>> GetNotConcluded(DateTime limitToComplete);
        void Add(Entities.Task task);
        void Delete(Entities.Task task);
        void Update(Entities.Task task);
    }
}
