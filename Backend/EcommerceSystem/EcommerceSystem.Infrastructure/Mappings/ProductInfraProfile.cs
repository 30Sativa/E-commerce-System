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
    public class ProductInfraProfile : Profile
    {
        public ProductInfraProfile()
        {
            CreateMap<Product, ProductEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Productid))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Categoryid))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Createdat))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Updatedat))
                .ReverseMap()
                .ForMember(dest => dest.Productid, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Categoryid, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Createdat, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Updatedat, opt => opt.MapFrom(src => src.UpdatedAt));
        }
    }
}
