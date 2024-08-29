using Domain.Entities;
using Domain.Interfaces;

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
            var task = new Entities.Task(command.UserId, command.Title, command.Description, command.LimitToComplete);
            _taskRepository.Add(task);

            var record = new Notification(task.UserId, task.Id, "Nova tarefa criada");
            _notificationRepository.Add(record);

            return task.Id;
        }
    }
}
