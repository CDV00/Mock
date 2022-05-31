using AutoMapper;
using Course.BLL.DTO;
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
            CreateMap<Lecture, LectureForCreateRequest>().ReverseMap();
            CreateMap<Lecture, LectureForUpdateRequest>().ReverseMap();
            CreateMap<Lecture, LectureDTO>().ReverseMap();
            //CreateMap<LessonDTO, LessonForUpdateRequest>().ReverseMap();

            // section
            CreateMap<Section, SectionCreateRequest>().ForMember(des => des.Lecture, src => src.MapFrom(opt => opt.Lecture)).ReverseMap();
            CreateMap<Section, SectionUpdateRequest>().ReverseMap().ForMember(des => des.Lecture, src => src.MapFrom(opt => opt.Lesson)).ReverseMap();
            CreateMap<Section, SectionDTO>().ForMember(des => des.Lesson, src => src.MapFrom(opt => opt.Lecture)).ReverseMap();
            //CreateMap<SectionDTO, SectionUpdateRequest>().ReverseMap();

            // language
            CreateMap<AudioLanguage, AudioLanguageForCreateRequest>().ReverseMap();
            CreateMap<AudioLanguage, AudioLanguageDTO>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionForCreateRequest>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionDTO>().ReverseMap();
            CreateMap<Language, LanguageResponse>().ReverseMap();

            //course
            CreateMap<Courses, CourseForCreateRequest>().ForMember(des => des.AudioLanguages, opt => opt.MapFrom(src => src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();
            CreateMap<Courses, CourseForUpdateRequest>().ForMember(des => des.AudioLanguages, opt => opt.MapFrom(src => src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();

            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Courses, CoursesCardDTO>().ForMember(des => des.User, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Category, opt => opt.MapFrom(src => src.Category)).ReverseMap();

            CreateMap<AudioLanguageDTO, AudioLanguage>().ReverseMap();
            CreateMap<CloseCaptionDTO, CloseCaption>().ReverseMap();
            CreateMap<CourseDTO, Courses>().ForMember(des => des.AudioLanguages, opt => opt.MapFrom(src => src.AudioLanguages)).ForMember(des => des.CloseCaptions, opt => opt.MapFrom(src => src.CloseCaptions)).ReverseMap();

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
            CreateMap<LectureCompletionRequest, LectureCompletionResponse>().ReverseMap();
            CreateMap<LectureCompletionRequest, LectureCompletion>().ReverseMap();
            CreateMap<LectureCompletionUser, AppUser>().ReverseMap();
            CreateMap<LectureCompletionLession, Lecture>().ReverseMap();
            CreateMap<LectureCompletion, LectureCompletionResponse>().ForMember(des => des.LectureCompUser, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Lecture, opt => opt.MapFrom(src => src.Lecture)).ReverseMap();
            CreateMap<LectureCompletion, LectureCompletionRequest>().ReverseMap();

            //CourseReview
            CreateMap<CourseReview, CourseReviewRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewUpdateRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewResponse>().ReverseMap();
        }
    }
}
