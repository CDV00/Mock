using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public interface ICourseQuery : IQuery<Courses>
    {
        ICourseQuery FilterIsActive(bool? isActice);
        Task<List<Courses>> GetAllByUserIdAsync(Guid userId);
        ICourseQuery FilterById(Guid Id);
        Task<Courses> GetById(Guid Id);
        ICourseQuery IncludeUser();
        ICourseQuery IncludeLanguage();
        ICourseQuery IncludeCategory();
        ICourseQuery IncludeSection();
        ICourseQuery IncludeAssignment();
        ICourseQuery IncludeQuiz();
        ICourseQuery IncludeEnrolment();
        ICourseQuery FilterByOrderd(Guid userId);
        ICourseQuery FilterByCategoryId(List<Guid?> categoryId);
        ICourseQuery FilterByAudioLanguageIds(List<Guid?> AudioLanguageIds);
        ICourseQuery FilterByCloseCaptionIds(List<Guid?> closeCaptionIds);
        ICourseQuery FilterByLevelIds(List<Guid?> levelIds);
        ICourseQuery FilterByKeyword(string Keyword);
        ICourseQuery FilterByDiscount(bool? IsSeller);
        ICourseQuery IncludeLevel();
        ICourseQuery IncludeDiscount();
        ICourseQuery FilterByUserId(Guid? userId);
        ICourseQuery FilterByRating(int? Rate);
        ICourseQuery FilterByPrice(bool isFree, bool isDiscount, decimal MinPrice, decimal MaxPrice);
        ICourseQuery FilterByApprove();
        ICourseQuery IncludeOrder();
        ICourseQuery FilterByIds(List<Guid> Ids);
        ICourseQuery FilterStatus(Status? status);
        ICourseQuery FilterByEnroll(Guid userId);
        ICourseQuery FilterBySaved(StatusOfUser? status, Guid? userId);
        ICourseQuery FilterByAddedCart(StatusOfUser? status, Guid? userId);
        ICourseQuery FilterByEnrollmented(StatusOfUser? status, Guid? userId, bool? isEnroll);
        ICourseQuery IncludeQuizCompletion(Guid? userId);
        ICourseQuery FilterByEnrollOrOrderd(Guid userId);
        ICourseQuery IncludeAssignmentCompletion(Guid? userId);
        ICourseQuery FilterByPurchased(StatusOfUser? status, Guid? userId, bool? isPurchased);
        ICourseQuery FilterByOwner(bool? IsOwner, Guid? userId);
        ICourseQuery IncludeLectureCompletion(Guid? userId);
        ICourseQuery IncludeLectureAttachment();
    }
}