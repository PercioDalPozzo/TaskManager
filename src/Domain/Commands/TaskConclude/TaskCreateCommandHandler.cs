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

namespace Domain.Commands.Taskconclude
{
    public class TaskConcludeCommandHandler : ICommandHandler<TaskConcludeCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public TaskConcludeCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Handle(TaskConcludeCommand command)
        {
            var task = _taskRepository.GetById(command.Id);

            task.Conclude();

            _taskRepository.Update(task);            
        }
    }
}
