using Domain.Commands.TaskDelete;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.TaskDelete
{
    public class TaskDeleteCommandHandler : ICommandHandler<TaskDeleteCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public TaskDeleteCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Handle(TaskDeleteCommand command)
        {
            _taskRepository.Delete(command.Id);            
        }
    }
}
