using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Category;
using EcommerceSystem.Application.Features.Category.Commands;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace EcommerceSystem.Application.Features.Category.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, IDistributedCache cache)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //DTO to Entity
            var entity = _mapper.Map<CategoryEntity>(request.Category);
            //insert db
            var create = await _categoryRepository.AddAsync(entity);
            //Xóa cache
            await _cache.RemoveAsync("categories:all", cancellationToken);
            return _mapper.Map<CategoryResponse>(create);
        }
    }
}
