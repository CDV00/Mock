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


            // lesion
            CreateMap<Lesson, LessonRequest>().ReverseMap();

            // section
            CreateMap<Section, SectionResponse>().ForMember(des => des.LessonRequest, opt => opt.MapFrom(src => src.Lessons)).ReverseMap().ReverseMap();
            CreateMap<Section, SectionRequest>().ForMember(des => des.LessonRequests, opt => opt.MapFrom(src => src.Lessons)).ReverseMap();
            CreateMap<Section, SectionUpdateRequest>().ForMember(des => des.LessonRequests, opt => opt.MapFrom(src => src.Lessons)).ReverseMap().ReverseMap();

            //course
            CreateMap<Courses, CourseRequest>().ReverseMap();
            CreateMap<CoursesRequest, CoursesResponse>().ReverseMap();

            CreateMap<Courses, CoursesResponse>().ReverseMap();
            CreateMap<AppUser, UserCourseResponse>().ReverseMap();
            CreateMap<Category, CategoryCourseRespones>().ReverseMap();

            CreateMap<Courses, CoursesResponse>().ForMember(des => des.UserResponse, opt => opt.MapFrom(src => src.User)).ForMember(des => des.CategoryResponse, opt => opt.MapFrom(src => src.Category)).ReverseMap();

            CreateMap<CourseRequest, Courses>().ForMember(des => des.Sections, opt => opt.MapFrom(src => src.SectionRequests));


            // map cart
            CreateMap<CartRequest, CartResponse>().ReverseMap();
            CreateMap<CartUser, AppUser>().ReverseMap();
            CreateMap<CartCourse, Courses>().ReverseMap();
            CreateMap<CartCategory, Category>().ReverseMap();
            CreateMap<ShoppingCart, CartResponse>().ForMember(des=>des.CartUser,opt=>opt.MapFrom(src=>src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course)).ForPath(des=>des.Course.category,opt=>opt.MapFrom(src=>src.Course.Category)).ReverseMap();
            CreateMap<ShoppingCart, CartRequest>().ReverseMap();


            CreateMap<CourseRequest, CoursesResponse>().ReverseMap();


            // map enrollment
            CreateMap<EnrollmentRequest, EnrollmentResponse>().ReverseMap();
            CreateMap<EnrollmentUser, AppUser>().ReverseMap();
            CreateMap<EnrollmentCourse, Courses>().ReverseMap();
            CreateMap<EnrollmentCategory, Category>().ReverseMap();
            CreateMap<Enrollment, EnrollmentResponse>().ForMember(des => des.EnrollmentUser, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Courses)).ForPath(des => des.Course.category, opt => opt.MapFrom(src => src.Courses.Category)).ReverseMap();
            CreateMap<Enrollment, EnrollmentRequest>().ReverseMap();

            // map course completion
            CreateMap<CourseCompletionRequest, CourseCompletionResponse>().ReverseMap();
            CreateMap<CourseCompletionUser, AppUser>().ReverseMap();
            CreateMap<CourseCompletionCourse, Courses>().ReverseMap();
            CreateMap<CourseCompletionCategory, Category>().ReverseMap();
            CreateMap<CourseCompletion, CourseCompletionResponse>().ForMember(des => des.CourseCompUser, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course)).ForPath(des => des.Course.category, opt => opt.MapFrom(src => src.Course.Category)).ReverseMap();
            CreateMap<CourseCompletion, CourseCompletionRequest>().ReverseMap();

            // map lesson completion
            CreateMap<LessonCompletionRequest, LessonCompletionResponse>().ReverseMap();
            CreateMap<LessonCompletionUser, AppUser>().ReverseMap();
            CreateMap<LessonCompletionLession, Lesson>().ReverseMap();
            CreateMap<LessonCompletion, LessonCompletionResponse>().ForMember(des => des.LessonCompUser, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Lesson, opt => opt.MapFrom(src => src.Lesson)).ReverseMap();
            CreateMap<LessonCompletion, LessonCompletionRequest>().ReverseMap();


            CreateMap<Section, SectionRequest>().ReverseMap();
            CreateMap<AudioLanguage, AudioLanguageRequest>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionRequest>().ReverseMap();
        }
    }
}
