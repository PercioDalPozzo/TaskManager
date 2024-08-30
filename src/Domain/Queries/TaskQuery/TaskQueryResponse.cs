namespace Domain.Commands.TaskQuery
{
    public record TaskQueryResponse(IReadOnlyList<Entities.Task> Records)
    {
    }
}
