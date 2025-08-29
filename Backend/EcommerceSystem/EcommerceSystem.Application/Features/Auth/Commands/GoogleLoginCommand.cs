using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Responses.Auth;
using MediatR;

namespace EcommerceSystem.Application.Features.Auth.Commands
{
    public class GoogleLoginCommand : IRequest<AuthResponse>
    {
        public string IdToken { get; set; }
        public GoogleLoginCommand(string idToken)
        {
            IdToken = idToken;
        }
    }
    
}
