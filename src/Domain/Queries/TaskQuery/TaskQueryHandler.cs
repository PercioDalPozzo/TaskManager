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

namespace Domain.Commands.TaskQuery
{
    public class TaskQueryHandler : IQueryHandler<TaskQuery, TaskQueryResponse>
    {
        private readonly ITaskRepository _taskRepository;

        public TaskQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

      
        public TaskQueryResponse Handle(TaskQuery query)
        {
            var tasks = _taskRepository.GetAllByUserId(query.Id);

            return new TaskQueryResponse(tasks);            
        }
    }
}
