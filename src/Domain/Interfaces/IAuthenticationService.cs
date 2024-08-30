using Domain.Services.Dto;

namespace Domain.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationResponse Valid(string login, string password);
        string Encrypt(string value);
    }
}
