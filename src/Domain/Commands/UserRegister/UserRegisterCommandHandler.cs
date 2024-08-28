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
    public class UserRegisterCommandHandler : ICommandResultHandler<UserRegisterCommand,Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserRegisterCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public Guid Handle(UserRegisterCommand command)
        {
            var encryptedPassword = _authenticationService.Encrypt(command.Password);

            var user = new User(command.Login, encryptedPassword);

            _userRepository.Add(user);

            return user.Id;
        }
    }
}
