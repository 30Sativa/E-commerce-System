using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Voucher;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Mappings
{
    public class VoucherAppProfile : Profile
    {
        public VoucherAppProfile()
        {
            CreateMap<VoucherEntity, VoucherResponse>().ReverseMap();
        }
    }
}
