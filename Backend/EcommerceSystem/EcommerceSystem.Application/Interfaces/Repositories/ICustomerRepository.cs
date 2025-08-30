using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity?> GetByIdAsync(int id);

        Task<CustomerEntity?> GetByEmailAsync(string email);
        Task<CustomerEntity> AddAsync(CustomerEntity customerEntity);
        Task<bool> UpdateAsync(CustomerEntity customerEntity);
        Task DeleteAsync(CustomerEntity customerEntity);
        Task<int> SaveChangesAsync();
    }
}
