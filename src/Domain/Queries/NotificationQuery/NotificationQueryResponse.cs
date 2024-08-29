using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Commands.NotificationQuery
{
    public record NotificationQueryResponse(IEnumerable<Notification> Records)
    {    
    }
}
