using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Interfaces.Repositories
{
    public interface IProductRepository 
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<ProductEntity?> GetByIdAsync(int id);
        Task<ProductEntity> AddAsync(ProductEntity productEntity);
        Task<bool> UpdateAsync(ProductEntity productEntity);
        Task DeleteAsync(ProductEntity productEntity);
        Task<int> SaveChangesAsync();
    }
}
