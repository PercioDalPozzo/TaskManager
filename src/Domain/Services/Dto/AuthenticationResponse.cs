namespace Domain.Services.Dto
{
    public record AuthenticationResponse(bool Valid, string UserId)
    {
    }
}
