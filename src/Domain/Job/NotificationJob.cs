using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Domain.Job
{
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationJob(ILogger<NotificationJob> logger, ITaskRepository taskRepository, INotificationRepository notificationRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _notificationRepository = notificationRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var limitToComplete = DateTime.Now.AddDays(1);
            var tasks = _taskRepository.GetNotConcluded(limitToComplete);

            foreach (var task in tasks)
            {
                _logger.LogInformation($"Gerando notificação para task {task.Id}");

                var notification = new Domain.Entities.Notification(task.UserId, task.Id, $"Tarefa pendente [{task.Title}]");
                _notificationRepository.Add(notification);
            }


            return Task.CompletedTask;
        }
    }
}
