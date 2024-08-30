using Domain.Interfaces;

namespace Domain.Commands.NotificationQuery
{
    public class NotificationQueryHandler : IQueryHandler<NotificationQuery, NotificationQueryResponse>
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }


        public async Task<NotificationQueryResponse> Handle(NotificationQuery query)
        {
            var records = await _notificationRepository.GetNotReadByUserId(query.UserId);

            return new NotificationQueryResponse(records);
        }
    }
}
