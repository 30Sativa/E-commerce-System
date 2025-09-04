using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Application.Features.Category.Commands;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace EcommerceSystem.Application.Features.Category.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDistributedCache _cache;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IDistributedCache cache)
        {
            _categoryRepository = categoryRepository;
            _cache = cache;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.id);
            if (category == null)
            {
                return false;
            }
            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();
            await _cache.RemoveAsync("categories:all", cancellationToken);
            return true;
        }
    }
}
