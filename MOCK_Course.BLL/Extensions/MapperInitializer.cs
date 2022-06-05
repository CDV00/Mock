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
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryUpdateRequest>().ReverseMap();

            CreateMap<RegisterRequest, AppUser>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email)).ReverseMap();
            CreateMap<AppUser, UserDTO>().ForMember(des => des.FullName, src => src.MapFrom(opt => opt.FirstName + opt.Fullname)).ReverseMap();

            // level
            CreateMap<Level, CourseLevelDTO>().ReverseMap();

            // lesion
            CreateMap<Lecture, LectureForCreateRequest>().ReverseMap();
            CreateMap<Lecture, LectureForUpdateRequest>().ReverseMap();
            CreateMap<Lecture, LectureDTO>().ReverseMap();
            CreateMap<Lecture, LectureForDetailDTO>().ReverseMap();

            // section
            CreateMap<Section, SectionCreateRequest>().ForMember(des => des.Lectures, src => src.MapFrom(opt => opt.Lectures)).ReverseMap();
            CreateMap<Section, SectionUpdateRequest>().ReverseMap().ForMember(des => des.Lectures, src => src.MapFrom(opt => opt.Lectures)).ReverseMap();
            CreateMap<Section, SectionDTO>().ForMember(des => des.Lectures, src => src.MapFrom(opt => opt.Lectures)).ReverseMap();
            CreateMap<Section, SectionForDetailDTO>().ForMember(des => des.Lectures, src => src.MapFrom(opt => opt.Lectures)).ReverseMap().ReverseMap();

            // language
            CreateMap<AudioLanguage, AudioLanguageForCreateRequest>().ReverseMap();
            CreateMap<AudioLanguage, AudioLanguageDTO>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionForCreateRequest>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionDTO>().ReverseMap();
            //CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<CourseLevelForCreateRequest, Level>().ReverseMap();

            //course
            CreateMap<Courses, CourseForCreateRequest>().ReverseMap();
            CreateMap<Courses, CourseForUpdateRequest>().ReverseMap();

            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<Category, CourseCategoryDTO>().ReverseMap();
            CreateMap<Courses, CoursesCardDTO>().ReverseMap();

            CreateMap<AudioLanguageDTO, AudioLanguage>().ReverseMap();
            CreateMap<CourseDTO, Courses>().ReverseMap();
            CreateMap<AppUser, CourseDetailUserDTO>().ReverseMap();
            CreateMap<CourseForDetailDTO, Courses>().ReverseMap();

            // map cart
            CreateMap<CartRequest, CartDTO>().ReverseMap();
            CreateMap<CartUserDTO, AppUser>().ReverseMap();
            CreateMap<CartCourseDTO, Courses>().ReverseMap();
            CreateMap<CartCategoryDTO, Category>().ReverseMap();
            CreateMap<ShoppingCart, CartDTO>().ForMember(des => des.Cart, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course)).ForPath(des => des.Course.Category, opt => opt.MapFrom(src => src.Course.Category)).ReverseMap();
            CreateMap<ShoppingCart, CartRequest>().ReverseMap();

            // AppUser
            CreateMap<UpdateProfileRequest, AppUser>().ForMember(des => des.Fullname, opt => opt.MapFrom(src => src.FirstName + src.LastName)).ReverseMap();
            CreateMap<AppUser, ChangePasswordRequest>().ReverseMap();
            CreateMap<AppUser, UserProfileDTO>().ReverseMap();
            CreateMap<AppUser, ResetPasswordRequest>().ReverseMap();

            //Order
            CreateMap<Order, OrderRequest>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateRequest>().ReverseMap();


            // map enrollment
            CreateMap<EnrollmentRequest, EnrollmentDTO>().ReverseMap();
            CreateMap<EnrollmentUserDTO, AppUser>().ReverseMap();
            CreateMap<EnrollmentCourseDTO, Courses>().ReverseMap();
            CreateMap<EnrollmentCategoryDTO, Category>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDTO>().ReverseMap();

            // map course completion
            CreateMap<CourseCompletionRequest, CourseCompletionDTO>().ReverseMap();
            CreateMap<CourseCompletionUserDTO, AppUser>().ReverseMap();
            CreateMap<CourseCompletionCourseDTO, Courses>().ReverseMap();
            CreateMap<CourseCompletionCategoryDTO, Category>().ReverseMap();
            CreateMap<CourseCompletion, CourseCompletionDTO>().ForMember(des => des.User, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Course, opt => opt.MapFrom(src => src.Course)).ForPath(des => des.Course.Category, opt => opt.MapFrom(src => src.Course.Category)).ReverseMap();
            CreateMap<CourseCompletion, CourseCompletionRequest>().ReverseMap();

            // map lesson completion
            CreateMap<LectureCompletionRequest, LectureCompletionDTO>().ReverseMap();
            CreateMap<LectureCompletionRequest, LectureCompletion>().ReverseMap();
            CreateMap<LectureCompletionUserDTO, AppUser>().ReverseMap();
            CreateMap<LectureCompletionLessionDTO, Lecture>().ReverseMap();
            CreateMap<LectureCompletion, LectureCompletionDTO>().ForMember(des => des.User, opt => opt.MapFrom(src => src.User)).ForMember(des => des.Lecture, opt => opt.MapFrom(src => src.Lecture)).ReverseMap();
            CreateMap<LectureCompletion, LectureCompletionRequest>().ReverseMap();

            //CourseReview
            CreateMap<CourseReview, CourseReviewRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewUpdateRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewDTO>().ReverseMap();


            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscountForCreateRequest>().ReverseMap();
            CreateMap<Discount, DiscountForUpdateRequest>().ReverseMap();
            CreateMap<Discount, DiscountForCreateDTO>().ReverseMap();
            CreateMap<Discount, DiscountForUpdateDTO>().ReverseMap();



            CreateMap<SubscriptionRequest, SubscriptionDTO>().ReverseMap();
            CreateMap<SubscriptionUserDTO, AppUser>().ReverseMap();
            CreateMap<Subscription, SubscriptionDTO>().ForMember(des => des.SubscriptionUser, opt => opt.MapFrom(src => src.User)).ReverseMap();
            CreateMap<Subscription, SubscriptionRequest>().ReverseMap();

            //CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscounRequest>().ReverseMap();
            //CreateMap<Discount, DiscountForUpdateRequest>().ReverseMap();
        }
    }
}
