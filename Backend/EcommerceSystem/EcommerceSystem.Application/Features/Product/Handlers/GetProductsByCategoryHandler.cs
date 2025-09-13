using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Product;
using EcommerceSystem.Application.Features.Product.Queries;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.Product.Handlers
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;


        public GetProductsByCategoryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var list = await _productRepository.GetByCategoryAsync(request.category);
            return _mapper.Map<IEnumerable<ProductResponse>>(list);
        }
    }
}
