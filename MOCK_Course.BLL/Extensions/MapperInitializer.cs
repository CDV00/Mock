﻿using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.DAL.Models;

namespace Course.BLL.Extensions
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<CategoryRequest, CategoryResponse>().ReverseMap();
            CreateMap<RegisterRequest, AppUser>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email)).ReverseMap();

            CreateMap<ShoppingCart, CartResponse>().ReverseMap();
            CreateMap<ShoppingCart, CartRequest>().ReverseMap();
            CreateMap<CartRequest, CartResponse>().ReverseMap();
            CreateMap<CartUser, AppUser>().ReverseMap();
            //CreateMap<CartCourse, Course>().ReverseMap();
        }
    }
}
