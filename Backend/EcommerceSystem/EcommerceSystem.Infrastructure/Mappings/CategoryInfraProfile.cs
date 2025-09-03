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
    public class CategoryInfraProfile : Profile
    {
        public CategoryInfraProfile() 
        {
            CreateMap<Category, CategoryEntity>()
                .ForMember(dest => dest.Categoryid, opt => opt.MapFrom(src => src.Categoryid))
                .ReverseMap()
                .ForMember(dest => dest.Categoryid, opt => opt.MapFrom(src => src.Categoryid));
        }
    }
}
