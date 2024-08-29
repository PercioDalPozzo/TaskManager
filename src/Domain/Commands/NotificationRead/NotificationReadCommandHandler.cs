using Domain.Interfaces;

namespace Domain.Commands.NotificationRead
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
