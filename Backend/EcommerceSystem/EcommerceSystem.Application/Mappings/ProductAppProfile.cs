using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Requests.Product;
using EcommerceSystem.Application.DTOs.Requests.ProductImage;
using EcommerceSystem.Application.DTOs.Responses.Product;
using EcommerceSystem.Application.DTOs.Responses.ProductImage;
using EcommerceSystem.Domain.Entities;

namespace EcommerceSystem.Application.Mappings
{
    public class ProductAppProfile : Profile
    {
        public ProductAppProfile()
        {
            // Domain -> Response
            CreateMap<ProductEntity, ProductResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));


            // Request -> Domain
            CreateMap<CreateProductRequest, ProductEntity>();
            CreateMap<UpdateProductRequest, ProductEntity>();

            //ProductImage
            //Request -> Domain
            CreateMap<CreateProductImageRequest, ProductImageEntity>();

            // Domain -> Response
            CreateMap<ProductImageEntity, ProductImageResponse>()
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(s => s.Id));

        }
    }
}
