using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationContext _context;

        public NotificationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Notification? GetById(Guid id)
        {
            return _context.Notification.FirstOrDefault(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Notification>> GetNotReadByUserId(Guid userId)
        {
            return await _context.Notification.Where(p => !p.Read && p.UserId == userId).ToListAsync();
        }

        public void Add(Notification notification)
        {
            _context.Notification.Add(notification);
            _context.SaveChanges();
        }

        public void Update(Notification notification)
        {
            _context.Notification.Update(notification);
            _context.SaveChanges();
        }
    }
}
