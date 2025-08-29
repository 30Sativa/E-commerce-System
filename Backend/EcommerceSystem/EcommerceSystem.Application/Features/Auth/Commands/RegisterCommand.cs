using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Requests.Auth;
using MediatR;

namespace EcommerceSystem.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<bool>
    {
        public RegisterRequest Request { get; set; }
        public RegisterCommand(RegisterRequest request)
        {
            Request = request;
        }
    }
}
