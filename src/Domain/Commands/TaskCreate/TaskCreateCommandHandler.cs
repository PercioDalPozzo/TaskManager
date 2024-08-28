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
        private readonly INotificationRepository _notificationRepository;        

        public TaskCreateCommandHandler(ITaskRepository taskRepository, INotificationRepository notificationRepository)
        {
            _taskRepository = taskRepository;
            _notificationRepository = notificationRepository;            
        }

        public Guid Handle(TaskCreateCommand command)
        {
            var task = new Entity.Task(command.UserId,command.Title, command.Description,command.LimitToComplete);            
            _taskRepository.Add(task);
            

            var record = new Notification(task.UserId, task.Id, "Nova tarefa criada");
            _notificationRepository.Add(record);

            return task.Id;
        }
    }
}
