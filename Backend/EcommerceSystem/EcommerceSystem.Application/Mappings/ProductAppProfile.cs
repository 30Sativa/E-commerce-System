using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Requests.Product;
using EcommerceSystem.Application.DTOs.Responses.Product;
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
        }
    }
}
