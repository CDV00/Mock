using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;
using Course.DAL.DTOs;
using System;

namespace Course.BLL.Extensions
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO_>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryUpdateRequest>().ReverseMap();
            CreateMap<DiscountDTO, Discount>().ReverseMap();
            CreateMap<DiscountDTO_, Discount>().ReverseMap();


            CreateMap<RegisterRequest, AppUser>().ForMember(des => des.UserName,
                                                            src => src.MapFrom(opt => opt.Email)).ReverseMap();

            // level
            CreateMap<Level, CourseLevelDTO>().ReverseMap();

            // lesion
            CreateMap<Lecture, LectureForCreateRequest>().ReverseMap();
            CreateMap<Lecture, LectureForUpdateRequest>().ReverseMap();
            CreateMap<LectureForUpdateRequest, Lecture>().ForMember(des => des.Id, src => src.MapFrom(opt => opt.IsNew == true ? Guid.NewGuid() : opt.Id)).ReverseMap();
            CreateMap<Lecture, LectureDTO>().ReverseMap();

            // section
            CreateMap<Section, SectionCreateRequest>().ReverseMap();
            CreateMap<Section, SectionUpdateRequest>().ReverseMap().ForMember(des => des.Lectures, src => src.MapFrom(opt => opt.Lectures)).ReverseMap();
            CreateMap<Section, SectionDTO>().ForMember(des => des.Lectures, src => src.MapFrom(opt => opt.Lectures)).ReverseMap();

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

            CreateMap<AudioLanguageDTO, AudioLanguage>().ReverseMap();
            CreateMap<CourseDTO, Courses>().ReverseMap();
            CreateMap<CourseOfDiscountDTO, Courses>().ReverseMap();

            // map cart
            CreateMap<CartRequest, CartDTO>().ReverseMap();
            CreateMap<UserDTO, AppUser>().ReverseMap();
            CreateMap<CourseDTO, Courses>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<ShoppingCart, CartDTO>().ReverseMap();
            CreateMap<ShoppingCart, CartRequest>().ReverseMap();
            CreateMap<ShoppingCart, CartUpdateRequest>().ReverseMap();

            // AppUser
            CreateMap<UpdateProfileRequest, AppUser>().ReverseMap();
            CreateMap<AppUser, ChangePasswordRequest>().ReverseMap();
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<AppUser, ResetPasswordRequest>().ReverseMap();

            //Order
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Order, OrderRequest>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateRequest>().ReverseMap();


            // map enrollment
            CreateMap<EnrollmentRequest, EnrollmentDTO>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDTO>().ReverseMap();

            // map course completion
            CreateMap<CourseCompletion, CourseCompletionRequest>().ReverseMap();

            // map lesson completion
            CreateMap<LectureCompletionRequest, LectureCompletionDTO>().ReverseMap();
            CreateMap<LectureCompletionRequest, LectureCompletion>().ReverseMap();
            CreateMap<LectureCompletion, LectureCompletionDTO>().ReverseMap();
            CreateMap<LectureCompletion, LectureCompletionRequest>().ReverseMap();

            //CourseReview
            CreateMap<CourseReview, CourseReviewRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewUpdateRequest>().ReverseMap();
            CreateMap<CourseReview, CourseReviewDTO>().ReverseMap();


            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscountForCreateRequest>().ReverseMap();
            CreateMap<Discount, DiscountForUpdateRequest>().ReverseMap();


            CreateMap<SubscriptionRequest, SubscriptionDTO>().ReverseMap();
            CreateMap<SubscriptionUserDTO, AppUser>().ReverseMap();
            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();
            CreateMap<Subscription, SubscriptionRequest>().ReverseMap();

            CreateMap<UserDTO, Subscription>().ReverseMap();

            // map saved cart
            CreateMap<SavedCoursesRequest, SavedCoursesDTO>().ReverseMap();
            CreateMap<UserDTO, AppUser>().ReverseMap();
            CreateMap<CourseDTO, Courses>().ReverseMap();
            CreateMap<DiscountDTO, Discount>().ReverseMap();
            CreateMap<SavedCourses, SavedCoursesDTO>().ReverseMap();
            CreateMap<SavedCourses, SavedCoursesRequest>().ReverseMap();

            //map assignment

            CreateMap<Assignment, AssignmentDTO>().ReverseMap();
            CreateMap<Assignment, AssignmentRequest>().ReverseMap();
            CreateMap<Assignment, AssignmentForCreateRequest>().ReverseMap();
            CreateMap<Assignment, AssignmentForUpdateRequest>().ReverseMap();


            CreateMap<Attachment, AttachmentDTO>().ReverseMap();
            CreateMap<Attachment, AttachmentRequest>().ReverseMap();
            CreateMap<Attachment, AttachmentForCreateRequest>().ReverseMap();
            CreateMap<Attachment, AttachmentForUpdateRequest>().ReverseMap();
            CreateMap<AttachmentRequest, AttachmentDTO>().ReverseMap();
            CreateMap<AttachmentForCreateRequest, AttachmentDTO>().ReverseMap();

            //map question
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, QuestionRequest>().ReverseMap();
            CreateMap<Question, QuestionForCreateRequest>().ReverseMap();
            CreateMap<Question, QuestionForUpdateRequest>().ReverseMap();

            //map quiz
            CreateMap<Quiz, QuizDTO>().ReverseMap();
            CreateMap<Quiz, QuizRequest>().ReverseMap();
            CreateMap<Quiz, QuizForCreateRequest>().ReverseMap();
            CreateMap<Quiz, QuizForUpdateRequest>().ReverseMap();

            //map quiz option
            CreateMap<QuizOption, QuizOptionDTO>().ReverseMap();
            CreateMap<QuizOption, QuizOptionCreateForRequest>().ReverseMap();
            CreateMap<QuizOption, QuizOptionForUpdateRequest>().ReverseMap();

            //map quiz setting
            CreateMap<QuizSetting, QuizSettingDTO>().ReverseMap();
            CreateMap<QuizSetting, QuizSettingRequest>().ReverseMap();
            CreateMap<QuizSetting, QuizSettingForCreateRequest>().ReverseMap();
            CreateMap<QuizSetting, QuizOptionForUpdateRequest>().ReverseMap();

        }
    }
}
