﻿using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;
using Course.DAL.DTOs;
using Entities.DTOs;
using Entities.Models;

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
            CreateMap<LectureAttachment, LectureAttachmentDTO>().ReverseMap();
            CreateMap<LectureAttachment, LectureAttachmentForCreateRequest>().ReverseMap();
            CreateMap<LectureAttachment, LectureAttachmentForUpdateRequest>().ReverseMap();
            CreateMap<Lecture, LectureForCreateRequest>().ReverseMap();
            CreateMap<Lecture, LectureForUpdateRequest>().ReverseMap();
            CreateMap<Lecture, LectureDTO>().ForMember(des => des.isCompleted,
                                                            src => src.MapFrom(opt => opt.LectureCompletion.Count >= 1)).ReverseMap();

            // language
            CreateMap<AudioLanguage, AudioLanguageForCreateRequest>().ReverseMap();
            CreateMap<AudioLanguage, AudioLanguageDTO>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionForCreateRequest>().ReverseMap();
            CreateMap<CloseCaption, CloseCaptionDTO>().ReverseMap();
            CreateMap<CourseLevelForCreateRequest, Level>().ReverseMap();

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
            CreateMap<Order, EarningDTO>().ReverseMap();
            CreateMap<Order, StatementsDTO>().ReverseMap();
            //
            CreateMap<Deposit, DepositDTO>().ReverseMap();


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

            //map asignment completion
            CreateMap<AssignmentCompletion, AssignmentCompletionRequest>().ReverseMap();
            CreateMap<AssignmentCompletion, AssignmentCompletionDTO>().ReverseMap();


            CreateMap<QuizCompletion, QuizCompletionRequest>().ReverseMap();
            CreateMap<QuizCompletion, QuizCompletionDTO>().ReverseMap();

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
            CreateMap<Attachment, AttachmentDTO>().ReverseMap();
            CreateMap<Attachment, AttachmentRequest>().ReverseMap();
            CreateMap<Attachment, AttachmentForCreateRequest>().ReverseMap();
            CreateMap<AttachmentRequest, AttachmentDTO>().ReverseMap();
            CreateMap<AttachmentForCreateRequest, AttachmentDTO>().ReverseMap();
            CreateMap<Attachment, AttachmentForUpdateRequest>().ReverseMap();

            CreateMap<Assignment, AssignmentDTO>().ForMember(des => des.isCompleted, src => src.MapFrom(opt => opt.AssignmentCompletion.Count >= 1))
                                                  .ReverseMap();
            CreateMap<Assignment, AssignmentRequest>().ReverseMap();
            CreateMap<Assignment, AssignmentForCreateRequest>().ReverseMap();
            CreateMap<Assignment, AssignmentForUpdateRequest>().ReverseMap();

            //map quiz option
            CreateMap<QuizOption, QuizOptionDTO>().ReverseMap();
            CreateMap<QuizOption, QuizOptionCreateForRequest>().ReverseMap();
            CreateMap<QuizOptionForUpdateRequest, QuizOption>().ReverseMap();

            //map question
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, QuestionRequest>().ReverseMap();
            CreateMap<Question, QuestionForCreateRequest>().ReverseMap();
            CreateMap<Question, QuestionForUpdateRequest>().ReverseMap();

            //map quiz
            CreateMap<Quiz, QuizDTO>().ForMember(des => des.isCompleted, src => src.MapFrom(opt => opt.QuizCompletion.Count >= 1)).ReverseMap();
            CreateMap<Quiz, QuizRequest>().ReverseMap();
            CreateMap<Quiz, QuizForCreateRequest>().ReverseMap();
            CreateMap<Quiz, QuizForUpdateRequest>().ReverseMap();

            // section
            CreateMap<Section, SectionCreateRequest>().ReverseMap();
            CreateMap<SectionUpdateRequest, Section>().ReverseMap();
            CreateMap<Section, SectionDTO>().ReverseMap();

            //course
            CreateMap<Courses, CourseForCreateRequest>().ReverseMap();
            CreateMap<CourseForUpdateRequest, Courses>().ReverseMap();
            //CreateMap<CourseForUpdateRequest, JsonPatchDocument<CourseForUpdateRequest>>().ReverseMap();

            //notification
            CreateMap<RoomMessageDTO, Room>().ReverseMap();
            CreateMap<RoomDTO, Room>().ReverseMap();
        }
    }
}
