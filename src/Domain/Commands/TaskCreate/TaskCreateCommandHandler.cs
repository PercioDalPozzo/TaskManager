using Domain.Commands.TaskCreate;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.TaskCreate
{
    public class TaskCreateCommandHandler : ICommandResultHandler<TaskCreateCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly INotificationService _notificationService;

        public TaskCreateCommandHandler(ITaskRepository taskRepository, INotificationService notificationService)
        {
            _taskRepository = taskRepository;
            _notificationService = notificationService;
        }

        public Guid Handle(TaskCreateCommand command)
        {
            var task = new Entity.Task(command.UserId,command.Title, command.Description,command.LimitToComplete);
            
            _taskRepository.Add(task);

            _notificationService.AddNotificationByTask(task);

            return task.Id;
        }
    }
}
