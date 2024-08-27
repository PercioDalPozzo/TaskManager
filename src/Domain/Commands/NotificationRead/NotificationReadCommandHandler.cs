using Domain.Commands.Taskconclude;
using Domain.Commands.TaskConclude;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Taskconclude
{
    public class NotificationReadCommandHandler : ICommandHandler<NotificationReadCommand>
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void Handle(NotificationReadCommand command)
        {
            var notification = _notificationRepository.GetById(command.Id);

            notification.ToRead();

            _notificationRepository.Update(notification);            
        }
    }
}
