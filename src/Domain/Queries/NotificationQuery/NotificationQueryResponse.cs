using Domain.Entities;

namespace Domain.Commands.NotificationQuery
{
    public record NotificationQueryResponse(IReadOnlyList<Notification> Records)
    {
    }
}
