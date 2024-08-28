using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<Entity.Task> GetAllByUserId(Guid userId);
        void Add(Entity.Task task);
        void Delete(Guid id);
        Entity.Task GetById(Guid id);
        void Update(Entity.Task task);
    }
}
