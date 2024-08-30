using Domain.Entities;

namespace Domain.Interfaces
{
    public interface INotificationRepository
    {
        Notification? GetById(Guid id);
        IEnumerable<Notification> GetNotReadByUserId(Guid userId);
        void Add(Notification notification);
        void Update(Notification notification);
    }
}
