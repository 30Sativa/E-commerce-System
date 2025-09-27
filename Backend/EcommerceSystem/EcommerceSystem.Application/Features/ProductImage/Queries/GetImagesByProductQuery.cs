using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Responses.ProductImage;
using MediatR;

namespace EcommerceSystem.Application.Features.ProductImage.Queries
{
    public record GetImagesByProductQuery(int ProductId) : IRequest<IEnumerable<ProductImageResponse>>
    {
    }
}
