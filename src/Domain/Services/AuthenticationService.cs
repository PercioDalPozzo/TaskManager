using Domain.Interfaces;
using Domain.Services.Dto;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthenticationResponse Valid(string login, string password)
        {
            var user = _userRepository.GetByLogin(login);
            if (user == null)
                return new AuthenticationResponse(false, "");

            var passwordCript = Encrypt(password);

            if (user.Password == passwordCript)
                return new AuthenticationResponse(true, user.Id.ToString());

            return new AuthenticationResponse(false, "");
        }

        public string Encrypt(string value)
        {
            var inputBytes = Encoding.ASCII.GetBytes(value);
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
