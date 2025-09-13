using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Responses.Order;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Mappings
{
    public class OrderAppProfile : Profile
    {
        public OrderAppProfile()
        {
            CreateMap<OrderEntity, OrderResponse>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailResponse>();
        }
    }
}
