using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Interfaces.Repositories
{
    public interface IVoucherRepository
    {
        Task<VoucherEntity?> GetByIdAsync(int id);
    }
}
