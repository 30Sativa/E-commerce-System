using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSystem.Application.DTOs.Requests.Customer;
using EcommerceSystem.Application.DTOs.Responses.Customer;
using EcommerceSystem.Domain.Entities;


namespace EcommerceSystem.Application.Mappings
{
    public class CustomerAppProfile : Profile
    {
        public CustomerAppProfile()
        {
            // Domain -> Response
            CreateMap<CustomerEntity, CustomerResponse>();
            CreateMap<CustomerEntity, CreateCustomerResponse>()
                .ForMember(dest => dest.Customerid, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role)); 
            CreateMap<CustomerEntity, UpdateCustomerResponse>();
            CreateMap<CustomerEntity, ChangePasswordResponse>()
                .ForMember(dest => dest.Customerid, opt => opt.MapFrom(src => src.Id));





            // Request -> Domain
            CreateMap<CreateCustomerRequest, CustomerEntity>();
            CreateMap<UpdateCustomerRequest, CustomerEntity>();
            CreateMap<CustomerRequest, CustomerEntity>();
            CreateMap<ChangePasswordRequest, CustomerEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));


        }
    }
}
