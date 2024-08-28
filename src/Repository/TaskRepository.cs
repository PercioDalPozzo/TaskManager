using Domain.Entity;
using Domain.Interfaces;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationContext _context;

        public TaskRepository(ApplicationContext context)
        {
            _context = context;
        }


        public IEnumerable<Domain.Entity.Task> GetOpen(DateTime limitToComplete)
        {
            return _context.Task
                .Where(p => !p.Concluded && p.LimitToComplete <= limitToComplete)
                .ToList();
        }

        IEnumerable<Domain.Entity.Task> ITaskRepository.GetAllByUserId(Guid userId)
        {
            return _context.Task
                .Where(p => p.UserId == userId)
                .ToList();
        }

        public void Add(Domain.Entity.Task task)
        {
            _context.Task.Add(task);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _context.Task.Remove(GetById(id));
            _context.SaveChanges();
        }


        public Domain.Entity.Task GetById(Guid id) => _context.Task.FirstOrDefault(p => p.Id == id);

        public void Update(Domain.Entity.Task task)
        {
            _context.Task.Update(task);
            _context.SaveChanges();
        }
    }
}
