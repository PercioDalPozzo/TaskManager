﻿using Api.Dtos;
using Domain.Commands.UserRegister;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class UsersController : ControllerBase
    {
        private readonly ICommandResultHandler<UserRegisterCommand, Guid> _handler;
        private readonly IAuthenticationService _authService;

        public UsersController(ICommandResultHandler<UserRegisterCommand, Guid> handler, IAuthenticationService authService)
        {
            _handler = handler;
            _authService = authService;
        }


        [HttpPost("register")]
        public IActionResult Register(UserRegisterCommand command)
        {
            var response = _handler.Handle(command);
            return Ok(new { UserId = response });
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto dto)
        {
            if (!_authService.Valid(dto.Username, dto.Password))
                return Unauthorized();

            var token = GenerateJwtToken(dto.Username);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtUtil.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "TaskManager.com",
                audience: "TaskManager.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
