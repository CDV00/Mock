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
            CreateMap<Category, CategoryUpdateRequest>().ReverseMap();

            CreateMap<RegisterRequest, AppUser>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email)).ReverseMap();
            CreateMap<UserResponse, AppUser>().ReverseMap();

            // lesion
            CreateMap<Lesson, LessonCreateRequest>().ReverseMap();
            CreateMap<Lesson, LessonUpdateRequest>().ReverseMap();

            // section
            CreateMap<Section, SectionResponse>().ReverseMap();
            CreateMap<Section, SectionCreateRequest>().ReverseMap();
            CreateMap<Section, SectionUpdateRequest>().ReverseMap();

            // language
            CreateMap<AudioLanguage, AudioLanguageCreateRequest>().ReverseMap();
            CreateMap<AudioLanguage, AudioLanguageCreateResponse>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionCreateRequest>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionCreateResponse>().ReverseMap();
            CreateMap<Language, LanguageResponse>().ReverseMap();

            //course
            CreateMap<Courses, CourseRequest>().ForMember(des=>des.AudioLanguages,opt=>opt.MapFrom(src=>src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();
            CreateMap<Courses, UpdateCourseRequest>().ForMember(des => des.AudioLanguages, opt => opt.MapFrom(src => src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();
            CreateMap<AppUser, UserCourseResponse>().ReverseMap();
            CreateMap<Category, CategoryCourseRespones>().ReverseMap();
            CreateMap<Courses, CoursesCartResponse>().ForMember(des => des.UserResponse, opt => opt.MapFrom(src => src.User)).ForMember(des => des.CategoryResponse, opt => opt.MapFrom(src => src.Category)).ReverseMap();
            CreateMap<CourseResponse, Courses>().ReverseMap();

            // map cart
            CreateMap<CartRequest, CartResponse>().ReverseMap();
            CreateMap<CartUserResponse, AppUser>().ReverseMap();
            CreateMap<CartCourseResponse, Courses>().ReverseMap();
            CreateMap<CartCategory, Category>().ReverseMap();
            CreateMap<ShoppingCart, CartResponse>().ForMember(des=>des.CartUser,opt=>opt.MapFrom(src=>src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course)).ForPath(des=>des.Course.category,opt=>opt.MapFrom(src=>src.Course.Category)).ReverseMap();
            CreateMap<ShoppingCart, CartRequest>().ReverseMap();
        }
    }
}
