﻿using Api.Controllers.Dto;
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
            _deleteHandler= deleteHandler;
            _concludeHandler = concludeHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{userId}")]
        public IActionResult GetAll(string userId)
        {
            var records = _queryHandler.Handle(new TaskQuery(Guid.Parse(userId)));
            return new OkObjectResult(records);
        }

        [HttpPost]
        public IActionResult Post(TaskCreateCommand command)
        {
            var id = _createHandler.Handle(command);
            return new OkObjectResult(new CreateResponseDto(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _deleteHandler.Handle(new TaskDeleteCommand(Guid.Parse(id)));
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Conclude(string id)
        {
            _concludeHandler.Handle(new TaskConcludeCommand(Guid.Parse(id)));
            return Ok();
        }
    }   
}
