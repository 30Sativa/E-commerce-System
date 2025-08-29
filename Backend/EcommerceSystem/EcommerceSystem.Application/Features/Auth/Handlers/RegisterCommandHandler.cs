using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.Features.Auth.Commands;
using EcommerceSystem.Application.Interfaces;
using MediatR;

namespace EcommerceSystem.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return _authService.RegisterAsync(request.Request);
        }
    }
}
