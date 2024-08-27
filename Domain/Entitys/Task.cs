using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Task : Entity
    {
        public Task(Guid userId, string title, string description, DateTime limitToComplete)
        {
            UserId = userId;        
            Title = title;
            Description = description;
            LimitToComplete = limitToComplete;
        }

        public Guid UserId { get; private set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LimitToComplete { get; set; }

        public bool Concluded { get; private set; } = false;


        public void Conclude()
        {
            Concluded = true;
        }
    }
}
