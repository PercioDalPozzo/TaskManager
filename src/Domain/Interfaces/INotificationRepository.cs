using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INotificationRepository
    {
        Notification GetById(Guid id);
        IEnumerable<Notification> GetNotReadByUserId(Guid userId);
        void Add(Notification notification);
        void Update(Notification notification);
    }
}
