using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Category;
using EcommerceSystem.Application.Features.Category.Commands;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace EcommerceSystem.Application.Features.Category.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, IDistributedCache cache)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _cache = cache;
        }


        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.id);
            if (category == null)
            {
                return null;
            }
            if(!string.IsNullOrEmpty(request.Category.Name))
            {
                category.Name = request.Category.Name;
            }
            await _categoryRepository.UpdateAsync(category);
            await _cache.RemoveAsync("categories:all", cancellationToken);
            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
