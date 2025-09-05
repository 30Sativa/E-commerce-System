using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderEntity> AddAsync(OrderEntity order);
        Task<OrderEntity?> GetByIdAsync(int id);
        Task<List<OrderEntity>> GetAllAsync();
        Task<List<OrderEntity>> GetByCustomerIdAsync(int customerId);
        Task<OrderEntity> UpdateAsync(OrderEntity order);
    }
}
