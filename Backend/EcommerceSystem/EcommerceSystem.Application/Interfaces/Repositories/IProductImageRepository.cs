using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Interfaces.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImageEntity>> GetByProductIdAsync(int productId);
        Task<ProductImageEntity?> GetByIdAsync(int imageId);
        Task<ProductImageEntity> AddAsync(ProductImageEntity entity); // không SaveChanges tại đây
        Task<bool> DeleteAsync(int imageId);                          // không SaveChanges tại đây
        Task<bool> SetMainAsync(int productId, int imageId);          // đảm bảo chỉ 1 ảnh IsMain
    }
}
