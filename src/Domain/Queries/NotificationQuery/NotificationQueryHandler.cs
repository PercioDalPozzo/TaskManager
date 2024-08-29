using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Commands.NotificationQuery
{
    public class NotificationQueryHandler : IQueryHandler<NotificationQuery, NotificationQueryResponse>
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

      
        public NotificationQueryResponse Handle(NotificationQuery query)
        {
            var records = _notificationRepository.GetNotReadByUserId(query.UserId);

            return new NotificationQueryResponse(records);            
        }
    }
}
