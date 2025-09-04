using System.Text.Json;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Category;
using EcommerceSystem.Application.Features.Category.Queries;
using EcommerceSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace EcommerceSystem.Application.Features.Category.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetAllCategoriesHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IDistributedCache cache)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "categories:all";

            // 1. Thử lấy dữ liệu từ Redis
            var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<CategoryResponse>>(cachedData);
            }

            // 2. Nếu chưa có thì đọc DB
            var categories = await _categoryRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CategoryResponse>>(categories);

            // 3. Lưu vào cache với TTL 30 phút
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(result),
                options,
                cancellationToken);

            return result;
        }
    }
}
