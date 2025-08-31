using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Responses.Product;
using MediatR;

namespace EcommerceSystem.Application.Features.Product.Queries
{
    public record GetProductByIdQuery(int id) : IRequest<ProductResponse>
    {
    }
}
