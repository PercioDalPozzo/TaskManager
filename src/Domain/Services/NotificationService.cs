using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NotificationService : INotificationService
    {
       private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void AddNotificationByTask(Entity.Task task)
        {
            var record = new Notification(task.UserId,task.Id,task.Description);

            _notificationRepository.Add(record);
        }
    }
}
