using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity?> GetByIdAsync(int id);
        Task<CategoryEntity> AddAsync(CategoryEntity category);
        Task<bool> UpdateAsync(CategoryEntity category);
        Task DeleteAsync(CategoryEntity category);
        Task<int> SaveChangesAsync();

        
    }
}
