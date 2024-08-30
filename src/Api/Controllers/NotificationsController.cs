using Domain.Commands.NotificationQuery;
using Domain.Commands.NotificationRead;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    [Authorize]
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
        public ActionResult GetByUserId(string userId)
        {
            var records = _queryHandler.Handle(new NotificationQuery(Guid.Parse(userId)));
            return Ok(records);
        }


        [HttpPut("{id}/read")]
        public ActionResult Read(string id)
        {
            _notificationReadHandler.Handle(new NotificationReadCommand(Guid.Parse(id)));
            return Ok();
        }
    }
}
