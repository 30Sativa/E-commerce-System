using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Product;
using EcommerceSystem.Application.Features.Product.Commands;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.Product.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            var product = await _productRepository.GetByIdAsync(request.id);
            if (product == null) return null;

            if(!string.IsNullOrEmpty(request.ProductRequest.Name))
                product.Name = request.ProductRequest.Name;
            if (!string.IsNullOrEmpty(request.ProductRequest.Description))
                product.Description = request.ProductRequest.Description;
            if (request.ProductRequest.Price.HasValue)
                product.Price = request.ProductRequest.Price.Value;
            if (request.ProductRequest.Stock.HasValue)
                product.Stock = request.ProductRequest.Stock.Value;
            product.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            if (request.ProductRequest.CategoryId.HasValue)
                product.CategoryId = request.ProductRequest.CategoryId.Value;


            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<ProductResponse>(product);

        }
    }
}
