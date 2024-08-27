using Api.Controllers.Dto;
using Api.Dtos;
using Domain.Commands.TaskConclude;
using Domain.Commands.TaskCreate;
using Domain.Commands.TaskDelete;
using Domain.Commands.TaskQuery;
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
    public class NotificationsController : ControllerBase
    {
        private readonly ICommandHandler<NotificationReadCommand> _notificationReadHandler;
        private readonly IQueryHandler<TaskQuery, TaskQueryResponse> _queryHandler;

        public NotificationsController(
            ICommandHandler<NotificationReadCommand> notificationReadHandler,
            IQueryHandler<TaskQuery, TaskQueryResponse> queryHandler)
        {
            _notificationReadHandler = notificationReadHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUserId(string userId)
        {
            var records = _queryHandler.Handle(new TaskQuery(Guid.Parse(userId)));
            return new OkObjectResult(records);
        }

   
        [HttpPut("{id}/read")]
        public IActionResult Read(string id)
        {
            _notificationReadHandler.Handle(new NotificationReadCommand(Guid.Parse(id)));
            return Ok();
        }
    }   
}
