
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification : Entity
    {
        public Notification(Guid userId, Guid taskId, string message)
        {
            UserId = userId;
            TaskId = taskId;
            Message = message;
        }

        public Guid UserId { get; private set; }

        public Guid TaskId { get; private set; }

        public string Message { get; private set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public bool Read { get; private set; } = false;

        public DateTime? ReadDate { get; private set; } 


        public void ToRead()
        {
            Read = true;
            ReadDate = DateTime.Now;
        }
    }
}
