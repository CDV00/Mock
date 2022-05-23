using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Course.BLL.Extensions
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryUpdateRequest>().ReverseMap();
            CreateMap<CategoryRequest, CategoryResponse>().ReverseMap();

            CreateMap<RegisterRequest, AppUser>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email)).ReverseMap();
            CreateMap<UserResponse, AppUser>().ReverseMap();

            CreateMap<Courses, CourseRequest>().ReverseMap();
            CreateMap<CoursesRequest, CoursesResponse>().ReverseMap();

            CreateMap<Courses, CoursesResponse>().ReverseMap();
            CreateMap<AppUser, UserCourseResponse>().ReverseMap();
            CreateMap<Category, CategoryCourseRespones>().ReverseMap();

            CreateMap<Courses, CoursesResponse>().ForMember(des => des.UserResponse, opt => opt.MapFrom(src => src.User)).ForMember(des=>des.CategoryResponse,opt=>opt.MapFrom(src=>src.Category)).ReverseMap();

            CreateMap<CourseRequest, Courses>().ForMember(des => des.Sections, opt => opt.MapFrom(src => src.SectionRequests));

            CreateMap<Section, SectionResponse>().ReverseMap();
            CreateMap<Section, SectionRequest>().ReverseMap();
            CreateMap<SectionRequest, SectionResponse>().ReverseMap();
            CreateMap<Section, SectionUpdateRequest>().ReverseMap();
            CreateMap<Lesson, LessonRequest>().ReverseMap();


            // map cart
            CreateMap<CartRequest, CartResponse>().ReverseMap();
            CreateMap<CartUser, AppUser>().ReverseMap();
            CreateMap<CartCourse, Courses>().ReverseMap();
            CreateMap<CartCategory, Category>().ReverseMap();
            CreateMap<ShoppingCart, CartResponse>().ForMember(des=>des.CartUser,opt=>opt.MapFrom(src=>src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course)).ForPath(des=>des.Course.category,opt=>opt.MapFrom(src=>src.Course.Category)).ReverseMap();
            CreateMap<ShoppingCart, CartRequest>().ReverseMap();


            CreateMap<CourseRequest, CoursesResponse>().ReverseMap();

            CreateMap<Section, SectionRequest>().ReverseMap();
            CreateMap<AudioLanguage, AudioLanguageRequest>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionRequest>().ReverseMap();
        }
    }
}
