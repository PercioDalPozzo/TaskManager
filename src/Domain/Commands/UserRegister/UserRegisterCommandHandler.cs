using Domain.Entity;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.UserRegister
{
    public class UserRegisterCommandHandler : ICommandHandler<UserRegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authService;

        public UserRegisterCommandHandler(IUserRepository userRepository, IAuthenticationService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public void Handle(UserRegisterCommand command)
        {
            var encryptedPassword = _authService.Encrypt(command.Password);

            _userRepository.Add(new User(command.Login, encryptedPassword));
        }
    }
}
