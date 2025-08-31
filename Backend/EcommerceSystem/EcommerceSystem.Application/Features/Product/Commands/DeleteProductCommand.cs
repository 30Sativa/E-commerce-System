using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EcommerceSystem.Application.Features.Product.Commands
{
    public record DeleteProductCommand(int id) : IRequest<bool>
    {
    }
}
