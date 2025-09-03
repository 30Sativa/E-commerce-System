using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Requests.Category;
using EcommerceSystem.Application.DTOs.Responses.Category;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Mappings
{
    public class CategoryAppProfile : Profile
    {
        public CategoryAppProfile()
        {
            CreateMap<CategoryEntity, CategoryResponse>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Categoryid));

            CreateMap<CategoryRequest, CategoryEntity>()
                .ReverseMap();
        }
    }
}
