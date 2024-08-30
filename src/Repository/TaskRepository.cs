using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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


        public async Task<IReadOnlyList<Domain.Entities.Task>> GetNotConcluded(DateTime limitToComplete)
        {
            return await _context.Task
                .Where(p => !p.Concluded && p.LimitToComplete <= limitToComplete)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.Task>> GetAllByUserId(Guid userId)
        {
            return await _context.Task
                .Where(p => p.UserId == userId)
                .ToListAsync();
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


        public Domain.Entities.Task? GetById(Guid id) => _context.Task.FirstOrDefault(p => p.Id == id);

        public void Update(Domain.Entities.Task task)
        {
            _context.Task.Update(task);
            _context.SaveChanges();
        }
    }
}
