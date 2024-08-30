using Domain.Entities;
using Domain.Interfaces;
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

        public IEnumerable<Notification> GetNotReadByUserId(Guid userId)
        {
            return _context.Notification.Where(p => !p.Read && p.UserId == userId).ToList();
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
