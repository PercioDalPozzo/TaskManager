using Domain.Interfaces;

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
