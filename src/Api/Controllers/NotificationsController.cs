using Domain.Commands.NotificationQuery;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.NotificationRead;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    //[Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly ICommandHandler<NotificationReadCommand> _notificationReadHandler;
        private readonly IQueryHandler<NotificationQuery, NotificationQueryResponse> _queryHandler;

        public NotificationsController(
            ICommandHandler<NotificationReadCommand> notificationReadHandler,
            IQueryHandler<NotificationQuery, NotificationQueryResponse> queryHandler)
        {
            _notificationReadHandler = notificationReadHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUserId(string userId)
        {
            var records = _queryHandler.Handle(new NotificationQuery(Guid.Parse(userId)));
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
