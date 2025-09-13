using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.Interfaces.Repositories;
using EcommerceSystem.Domain.Entities;
using EcommerceSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VoucherRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<VoucherEntity?> GetByIdAsync(int id)
        {
            var dbvoucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Voucherid == id);
            return _mapper.Map<VoucherEntity?>(dbvoucher);
        }
    }
    
}
