using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Product;
using EcommerceSystem.Application.Features.Product.Commands;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.Product.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
       
            var entity = _mapper.Map<Domain.Entities.ProductEntity>(request.ProductRequest);
            entity.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            entity.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var created = await _productRepository.AddAsync(entity);
            return _mapper.Map<ProductResponse>(created);   
        }
    }
}
