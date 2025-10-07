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
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Productimages.FirstOrDefault(pi => pi.Ismain).Imageurl))
                .ReverseMap()
                .ForMember(dest => dest.Productid, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Categoryid, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Createdat, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Updatedat, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Productimages, opt => opt.Ignore());


            // Productimage <-> ProductImageEntity
            CreateMap<Productimage, ProductImageEntity>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Imageid))
                .ForMember(d => d.ProductId, m => m.MapFrom(s => s.Productid))
                .ForMember(d => d.ImageUrl, m => m.MapFrom(s => s.Imageurl))
                .ForMember(d => d.IsMain, m => m.MapFrom(s => s.Ismain))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(s => s.Createdat));

            CreateMap<ProductImageEntity, Productimage>()
                .ForMember(d => d.Imageid, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Productid, m => m.MapFrom(s => s.ProductId))
                .ForMember(d => d.Imageurl, m => m.MapFrom(s => s.ImageUrl))
                .ForMember(d => d.Ismain, m => m.MapFrom(s => s.IsMain))
                .ForMember(d => d.Createdat, m => m.MapFrom(s => s.CreatedAt));
        }
    }
}
