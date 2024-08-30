using Api.Controllers.Dto;
using Domain.Commands.TaskConclude;
using Domain.Commands.TaskCreate;
using Domain.Commands.TaskDelete;
using Domain.Commands.TaskQuery;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ICommandResultHandler<TaskCreateCommand, Guid> _createHandler;
        private readonly ICommandHandler<TaskDeleteCommand> _deleteHandler;
        private readonly ICommandHandler<TaskConcludeCommand> _concludeHandler;
        private readonly IQueryHandler<TaskQuery, TaskQueryResponse> _queryHandler;

        public TasksController(
            ICommandResultHandler<TaskCreateCommand, Guid> createHandler,
            ICommandHandler<TaskDeleteCommand> deleteHandler,
            ICommandHandler<TaskConcludeCommand> concludeHandler,
            IQueryHandler<TaskQuery, TaskQueryResponse> queryHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _concludeHandler = concludeHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAll(string userId)
        {
            var response = await _queryHandler.Handle(new TaskQuery(Guid.Parse(userId)));
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Post(TaskCreateCommand command)
        {
            var id = _createHandler.Handle(command);
            return Ok(new CreateResponseDto(id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            _deleteHandler.Handle(new TaskDeleteCommand(Guid.Parse(id)));
            return Ok();
        }

        [HttpPut("{id}/complete")]
        public ActionResult Complete(string id)
        {
            _concludeHandler.Handle(new TaskConcludeCommand(Guid.Parse(id)));
            return Ok();
        }
    }
}
