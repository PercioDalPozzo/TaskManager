using Domain.Entities;

namespace Domain.Interfaces
{
    public interface INotificationRepository
    {
        Notification? GetById(Guid id);
        Task<IReadOnlyList<Notification>> GetNotReadByUserId(Guid userId);
        void Add(Notification notification);
        void Update(Notification notification);
    }
}
