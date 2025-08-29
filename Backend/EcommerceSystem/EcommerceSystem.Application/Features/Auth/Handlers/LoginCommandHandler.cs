using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Responses.Auth;
using EcommerceSystem.Application.Features.Auth.Commands;
using EcommerceSystem.Application.Interfaces;
using MediatR;

namespace EcommerceSystem.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public Task<AuthResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            return _authService.LoginAsync(command.Request);
        }
    }
}
