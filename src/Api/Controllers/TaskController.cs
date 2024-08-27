using Api.Controllers.Dto;
using Api.Dtos;
using Domain.Commands.TaskCreate;
using Domain.Commands.TaskDelete;
using Domain.Commands.UserRegister;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    //[Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ICommandResultHandler<TaskCreateCommand, Guid> _handlerCreate;
        private readonly ICommandHandler<TaskDeleteCommand> _handlerDelete;

        public TasksController(ICommandResultHandler<TaskCreateCommand, Guid> handlerCreate, ICommandHandler<TaskDeleteCommand> handlerDelete)
        {
            _handlerCreate = handlerCreate;
            _handlerDelete = handlerDelete;
        }

        [HttpPost]
        public IActionResult Post(TaskCreateCommand command)
        {
            var id = _handlerCreate.Handle(command);
            return new OkObjectResult(new CreateResponseDto(id));
        }

        [HttpDelete]
        public IActionResult Delete(TaskDeleteCommand command)
        {
            _handlerDelete.Handle(command);
            return Ok();
        }


    }   
}
