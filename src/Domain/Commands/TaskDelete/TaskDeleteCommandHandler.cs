using Domain.Interfaces;

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
            var task = _taskRepository.GetById(command.Id);
            if (task != null)
                _taskRepository.Delete(task);
        }
    }
}
