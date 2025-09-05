using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Domain.Entities;
using EcommerceSystem.Infrastructure.Persistence.Models;

namespace EcommerceSystem.Infrastructure.Mappings
{
    public class VoucherInfraProfile : Profile
    {
        public VoucherInfraProfile()
        {
            CreateMap<Voucher, VoucherEntity>().ReverseMap();
        }
    }
}
