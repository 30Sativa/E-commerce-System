using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EcommerceSystem.Application.Features.ProductImage.Commands
{
    public record DeleteProductImageCommand(int ImageId) : IRequest<bool>
    {
    }
}
