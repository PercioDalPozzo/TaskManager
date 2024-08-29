using Domain.Interfaces;

namespace Domain.Commands.TaskConclude
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
