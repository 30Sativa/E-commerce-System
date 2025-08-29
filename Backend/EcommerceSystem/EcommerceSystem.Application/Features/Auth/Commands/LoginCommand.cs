using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Requests.Auth;
using EcommerceSystem.Application.DTOs.Responses.Auth;
using MediatR;



namespace EcommerceSystem.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public LoginRequest Request { get; set; }
        public LoginCommand(LoginRequest request)
        {
            Request = request;
        }
    }
    
}
