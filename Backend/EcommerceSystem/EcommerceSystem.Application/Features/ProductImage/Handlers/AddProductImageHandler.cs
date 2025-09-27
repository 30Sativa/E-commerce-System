using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.ProductImage;
using EcommerceSystem.Application.Features.ProductImage.Commands;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Entities;
using MediatR;

namespace EcommerceSystem.Application.Features.ProductImage.Handlers
{
    public class AddProductImageHandler : IRequestHandler<AddProductImageCommand, ProductImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;


        public AddProductImageHandler(IProductImageRepository productImageRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }


        public async Task<ProductImageResponse> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if(product == null)
            {
                throw new Exception($"Product with ID {request.ProductId} not found.");
            }
            var entity = _mapper.Map<ProductImageEntity>(request.request);
            entity.ProductId = request.ProductId;
            entity.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            var create = await _productImageRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<ProductImageResponse>(create);
        }
    }
}
