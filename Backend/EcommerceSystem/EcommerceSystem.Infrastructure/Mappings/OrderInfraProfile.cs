using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Domain.Common.Enum;
using EcommerceSystem.Domain.Entities;
using EcommerceSystem.Infrastructure.Persistence.Models;

namespace EcommerceSystem.Infrastructure.Mappings
{
    public class OrderInfraProfile : Profile
    {
        public OrderInfraProfile()
        {
            // Domain -> DB
            CreateMap<OrderEntity, Order>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<OrderDetailEntity, Orderdetail>();

            // DB -> Domain
            CreateMap<Order, OrderEntity>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    Enum.Parse<OrderStatus>(src.Status, true)));

            CreateMap<Orderdetail, OrderDetailEntity>();
        }
    }
}
