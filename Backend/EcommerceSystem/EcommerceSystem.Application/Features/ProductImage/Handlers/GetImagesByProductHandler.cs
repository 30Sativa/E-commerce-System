using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.ProductImage;
using EcommerceSystem.Application.Features.ProductImage.Queries;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.ProductImage.Handlers
{
    public class GetImagesByProductHandler : IRequestHandler<GetImagesByProductQuery, IEnumerable<ProductImageResponse>>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public GetImagesByProductHandler(IProductImageRepository productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductImageResponse>> Handle(GetImagesByProductQuery request, CancellationToken cancellationToken)
        {
            var images = await _productImageRepository.GetByIdAsync(request.ProductId);
            return _mapper.Map<IEnumerable<ProductImageResponse>>(images);
        }
    }
}
