using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public Notification GetById(Guid id)
        {
            //Mock
            return new Notification(Guid.NewGuid(), Guid.NewGuid(), "Message1");
        }

        public IEnumerable<Notification> GetNotReadByUserId(Guid userId)
        {
            //Mock
            return new List<Notification>()
            {
                new Notification(Guid.NewGuid(),Guid.NewGuid(),"Message1"),
                new Notification(Guid.NewGuid(),Guid.NewGuid(),"Message2"),
                new Notification(Guid.NewGuid(),Guid.NewGuid(),"Message3"),
                new Notification(Guid.NewGuid(),Guid.NewGuid(),"Message4")
            };
        }

        public void Add(Notification notification)
        {

        }

        public void Update(Notification notification)
        {
            
        }
    }
}
