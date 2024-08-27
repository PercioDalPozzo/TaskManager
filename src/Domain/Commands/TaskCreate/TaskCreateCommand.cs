using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.TaskCreate
{
    public class TaskCreateCommand
    {
        public Guid UserId { get;  set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime LimitToComplete { get; set; }
    }
}
