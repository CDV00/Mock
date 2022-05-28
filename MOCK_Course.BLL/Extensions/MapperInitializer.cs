using AutoMapper;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;

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
            CreateMap<AppUser, UserResponse>().ForMember(des => des.FullName, src => src.MapFrom(opt => opt.FirstName + opt.Fullname)).ReverseMap();

            // lesion
            CreateMap<Lesson, LessonCreateRequest>().ReverseMap();
            CreateMap<Lesson, LessonResponse>().ReverseMap();
            CreateMap<Lesson, LessonUpdateRequest>().ReverseMap();
            CreateMap<LessonResponse, LessonUpdateRequest>().ReverseMap();

            // section
            CreateMap<Section, SectionResponse>().ReverseMap();
            CreateMap<Section, SectionCreateRequest>().ReverseMap();
            CreateMap<Section, SectionUpdateRequest>().ReverseMap();
            CreateMap<SectionResponse, SectionUpdateRequest>().ReverseMap();

            // language
            CreateMap<AudioLanguage, AudioLanguageCreateRequest>().ReverseMap();
            CreateMap<AudioLanguage, AudioLanguageCreateResponse>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionCreateRequest>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionCreateResponse>().ReverseMap();
            CreateMap<Language, LanguageResponse>().ReverseMap();

            //course
            CreateMap<Courses, CourseRequest>().ForMember(des => des.AudioLanguages, opt => opt.MapFrom(src => src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();
            CreateMap<Courses, UpdateCourseRequest>().ForMember(des => des.AudioLanguages, opt => opt.MapFrom(src => src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();

            CreateMap<AppUser, UserCourseResponse>().ReverseMap();
            CreateMap<Category, CategoryCourseRespones>().ReverseMap();
            CreateMap<Courses, CoursesCardResponse>().ForMember(des => des.UserResponse, opt => opt.MapFrom(src => src.User)).ForMember(des => des.CategoryResponse, opt => opt.MapFrom(src => src.Category)).ReverseMap();

            CreateMap<AudioLanguageCreateResponse, AudioLanguage>().ReverseMap();
            CreateMap<CloseCaptionCreateResponse, CloseCaption>().ReverseMap();
            CreateMap<CourseResponse, Courses>().ForMember(des => des.AudioLanguages, opt => opt.MapFrom(src => src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();

            // map cart
            CreateMap<CartRequest, CartResponse>().ReverseMap();
            CreateMap<CartUserResponse, AppUser>().ReverseMap();
            CreateMap<CartCourseResponse, Courses>().ReverseMap();
            CreateMap<CartCategory, Category>().ReverseMap();
            CreateMap<ShoppingCart, CartResponse>().ForMember(des => des.CartUser, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course)).ForPath(des => des.Course.category, opt => opt.MapFrom(src => src.Course.Category)).ReverseMap();
            CreateMap<ShoppingCart, CartRequest>().ReverseMap();

            // AppUser
            CreateMap<AppUser, UpdateProfileRequest>().ReverseMap();
            CreateMap<AppUser, ChangePasswordRequest>().ReverseMap();
            CreateMap<AppUser, UserProfileResponse>().ReverseMap();

            //Order
            CreateMap<Order, OrderRequest>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Order, OrderUpdateRequest>().ReverseMap();

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
            CreateMap<LessonCompletionRequest, LessonCompletion>().ReverseMap();
            CreateMap<LessonCompletionUser, AppUser>().ReverseMap();
            CreateMap<LessonCompletionLession, Lesson>().ReverseMap();
            CreateMap<LessonCompletion, LessonCompletionResponse>().ForMember(des => des.LessonCompUser, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Lesson, opt => opt.MapFrom(src => src.Lesson)).ReverseMap();
            CreateMap<LessonCompletion, LessonCompletionRequest>().ReverseMap();

            //CourseReview
            CreateMap<CourseReview, CourseReviewRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewUpdateRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewResponse>().ReverseMap();
        }
    }
}
