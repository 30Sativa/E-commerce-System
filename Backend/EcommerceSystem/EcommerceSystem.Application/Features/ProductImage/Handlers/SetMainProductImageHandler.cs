using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.Features.ProductImage.Commands;
using EcommerceSystem.Application.Interfaces;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;

namespace EcommerceSystem.Application.Features.ProductImage.Handlers
{
    public class SetMainProductImageHandler : IRequestHandler<SetMainProductImageCommand, bool>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SetMainProductImageHandler(IProductImageRepository productImageRepository, IUnitOfWork unitOfWork)
        {
            _productImageRepository = productImageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SetMainProductImageCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            var image = await _productImageRepository.GetByIdAsync(request.ImageId);
            if (image == null) return false;
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
    }
}
