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
    public class CustomerInfraProfile : Profile
    {
        public CustomerInfraProfile()
        {
            // EF Models -> Doamin

            CreateMap<Customer, CustomerEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Customerid))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Passwordhash))
                .ForMember(dest => dest.GoogleId, opt => opt.MapFrom(src => src.Googleid))
                .ForMember(dest => dest.AuthProvider, opt => opt.MapFrom(src => src.Authprovider))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Createdat))
                .ReverseMap()
                .ForMember(dest => dest.Customerid, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Passwordhash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Googleid, opt => opt.MapFrom(src => src.GoogleId))
                .ForMember(dest => dest.Authprovider, opt => opt.MapFrom(src => src.AuthProvider))
                .ForMember(dest => dest.Createdat, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
