using Api.Controllers.Dto;
using Api.Dtos;
using Domain.Commands.TaskCreate;
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
        private readonly ICommandResultHandler<TaskCreateCommand, Guid> _handler;
        
        public TasksController(ICommandResultHandler<TaskCreateCommand, Guid> handler)
        {
            _handler = handler;       
        }

        [HttpPost]
        public IActionResult Post(TaskCreateCommand command)
        {
            var id = _handler.Handle(command);
            return new OkObjectResult(new CreateResponseDto(id));
        }

               
    }   
}
