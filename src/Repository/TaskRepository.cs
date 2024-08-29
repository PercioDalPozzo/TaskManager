using Domain.Interfaces;
using Repository.Context;

namespace Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationContext _context;

        public TaskRepository(ApplicationContext context)
        {
            _context = context;
        }


        public IEnumerable<Domain.Entities.Task> GetOpen(DateTime limitToComplete)
        {
            return _context.Task
                .Where(p => !p.Concluded && p.LimitToComplete <= limitToComplete)
                .ToList();
        }

        IEnumerable<Domain.Entities.Task> ITaskRepository.GetAllByUserId(Guid userId)
        {
            return _context.Task
                .Where(p => p.UserId == userId)
                .ToList();
        }

        public void Add(Domain.Entities.Task task)
        {
            _context.Task.Add(task);
            _context.SaveChanges();
        }

        public void Delete(Domain.Entities.Task task)
        {
            _context.Task.Remove(task);
            _context.SaveChanges();
        }


        public Domain.Entities.Task GetById(Guid id) => _context.Task.FirstOrDefault(p => p.Id == id);

        public void Update(Domain.Entities.Task task)
        {
            _context.Task.Update(task);
            _context.SaveChanges();
        }
    }
}
