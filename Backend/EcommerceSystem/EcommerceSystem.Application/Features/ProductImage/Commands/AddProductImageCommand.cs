using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.DTOs.Requests.Product;
using EcommerceSystem.Application.DTOs.Requests.ProductImage;
using EcommerceSystem.Application.DTOs.Responses.ProductImage;
using MediatR;

namespace EcommerceSystem.Application.Features.ProductImage.Commands
{
    public record AddProductImageCommand(int ProductId, CreateProductImageRequest request) : IRequest<ProductImageResponse>
    {
    }
}
