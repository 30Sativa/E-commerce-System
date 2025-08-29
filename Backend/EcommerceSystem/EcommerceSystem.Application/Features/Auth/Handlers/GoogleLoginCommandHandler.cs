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
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public Task<AuthResponse> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            return _authService.GoogleLoginAsync(request.IdToken);
        }
    }
}
