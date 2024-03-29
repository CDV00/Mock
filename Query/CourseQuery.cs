﻿using Course.DAL.Data;
using Course.DAL.Models;
using Entities.ParameterRequest;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public class CourseQuery : QueryBase<Courses>, ICourseQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public CourseQuery(IQueryable<Courses> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        public ICourseQuery FilterByOrderd(Guid userId)
        {
            Query = Query.Where(type => type.OrderItems.Any(o => o.Order.UserId == userId));
            return this;
        }

        /// <summary>
        /// FilterByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ICourseQuery FilterByUserId(Guid? userId)
        {
            if (userId == null)
                return this;

            Query = Query.Where(type => type.UserId == userId);
            return this;
        }

        public ICourseQuery FilterByOwner(bool? IsOwner, Guid? userId)
        {
            if (userId == null)
                return this;

            if (IsOwner.GetValueOrDefault())
                Query = Query.Where(type => type.UserId == userId);

            if (!IsOwner.GetValueOrDefault())
                Query = Query.Where(type => type.UserId != userId);

            return this;
        }


        public ICourseQuery FilterByApprove()
        {
            Query = Query.Where(type => type.status == Status.Aprrove);
            return this;
        }


        /// <summary>
        /// FilterIsActive
        /// </summary>
        /// <param name="isActice"></param>
        /// <returns></returns>
        public ICourseQuery FilterIsActive(bool? isActice)
        {
            if (isActice is null)
                return this;

            Query = Query.Where(type => type.IsActive == isActice);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ICourseQuery FilterById(Guid Id)
        {
            Query = Query.Where(type => type.Id == Id);
            return this;
        }
        public ICourseQuery FilterByEnroll(Guid userId)
        {
            Query = Query.Where(c => c.Enrollments.Any(e => e.UserId == userId));
            return this;
        }

        public ICourseQuery FilterByEnrollOrOrderd(Guid userId)
        {
            Query = Query.Where(c => c.Enrollments.Any(e => e.UserId == userId) || c.OrderItems.Any(o => o.Order.UserId == userId));
            return this;
        }

        public ICourseQuery FilterByIds(List<Guid> Ids)
        {
            Query = Query.Where(type => Ids.Contains(type.Id));
            return this;
        }

        public ICourseQuery FilterByPrice(bool isFree, bool isDiscount, decimal MinPrice, decimal MaxPrice)
        {
            if (isFree && isDiscount)
            {
                Query = Query.Where(type => type.IsFree == isFree || type.Discounts.Any(d => d.EndDate > DateTime.Now));
                return this;
            }

            if (isFree)
            {
                Query = Query.Where(type => type.IsFree == isFree);
            }

            if (!isFree || (isFree && isDiscount))
            {
                Query = Query.Where(type => type.Price >= MinPrice && type.Price <= MaxPrice);
            }


            if (isDiscount)
            {
                Query = Query.Where(type => type.Discounts.Any(d => d.EndDate > DateTime.Now));
            }

            return this;
        }

        public ICourseQuery IncludeUser()
        {
            Query.Include(c => c.User)
                 .Load();
            return this;
        }

        public ICourseQuery IncludeDiscount()
        {
            Query.Include(c => c.Discounts.Where(d => d.EndDate >= DateTime.UtcNow && d.StartDate <= DateTime.UtcNow))
                 .Load();

            return this;
        }
        public ICourseQuery IncludeOrder()
        {
            Query.Include(c => c.OrderItems)
                 .ThenInclude(o => o.Order)
                 .Load();

            return this;
        }


        public ICourseQuery IncludeLevel()
        {
            Query.Include(c => c.Levels)
                 .Load();
            return this;
        }

        public ICourseQuery IncludeEnrolment()
        {
            Query.Include(c => c.Enrollments).Load();
            return this;
        }
        public ICourseQuery IncludeLanguage()
        {
            Query.Include(c => c.AudioLanguages)
                 .Include(c => c.CloseCaptions)
                 .Load();
            return this;
        }

        public ICourseQuery FilterByCategoryId(List<Guid?> categoryId)
        {
            if (categoryId == null)
                return this;

            Query = Query.Where(c => categoryId.Contains(c.CategoryId));
            return this;
        }


        public ICourseQuery FilterByDiscount(bool? IsSeller)
        {
            if (IsSeller == null || IsSeller == false)
                return this;

            Query = Query.Where(c => c.Discounts.Any(d => d.EndDate > DateTime.Now));
            return this;
        }

        public ICourseQuery FilterByRating(int? Rate)
        {
            if (Rate == null)
                return this;

            Query = Query.Where(c => c.AvgRate >= Rate);

            return this;
        }

        public ICourseQuery FilterBySaved(StatusOfUser? status, Guid? userId)
        {
            if (status == null || status != StatusOfUser.IsSaved || userId == null)
                return this;


            Query = Query.Where(c => c.SavedCourses.Any(s => s.UserId == userId));
            return this;
        }

        public ICourseQuery FilterByAddedCart(StatusOfUser? status, Guid? userId)
        {
            if (status == null || status != StatusOfUser.IsCart || userId == null)
                return this;

            Query = Query.Where(c => c.Carts.Any(s => s.UserId == userId));
            return this;
        }

        public ICourseQuery FilterByEnrollmented(StatusOfUser? status, Guid? userId, bool? IsEnroll)
        {
            if (status == null || userId == null)
                return this;

            if (status == StatusOfUser.IsEnrollemt)
                Query = Query.Where(c => c.Enrollments.Any(s => s.UserId == userId));

            if (IsEnroll != null && IsEnroll == true)
                Query = Query.Where(c => c.Enrollments.Any(s => s.UserId == userId));

            if (IsEnroll != null && IsEnroll == false)
                Query = Query.Where(c => c.Enrollments.Any(s => s.UserId != userId));

            return this;
        }

        public ICourseQuery FilterByPurchased(StatusOfUser? status, Guid? userId, bool? isPurchased)
        {
            if (status == null || userId == null)
                return this;

            if (status == StatusOfUser.IsPurchased || isPurchased == true)
                Query = Query.Where(c => c.OrderItems.Any(s => s.Order.UserId == userId));

            return this;
        }




        public ICourseQuery FilterByAudioLanguageIds(List<Guid?> AudioLanguageIds)
        {
            if (AudioLanguageIds == null)
                return this;

            Query = Query.Where(c => c.AudioLanguages.Any(c => AudioLanguageIds.Contains(c.Id)));

            return this;
        }

        public ICourseQuery FilterByCloseCaptionIds(List<Guid?> closeCaptionIds)
        {
            if (closeCaptionIds == null)
                return this;
            Query = Query.Where(c => c.CloseCaptions.Any(c => closeCaptionIds.Contains(c.Id)));

            return this;
        }

        public ICourseQuery FilterByLevelIds(List<Guid?> levelIds)
        {
            if (levelIds == null)
                return this;

            Query = Query.Where(c => c.Levels.Any(c => levelIds.Contains(c.Id)));

            return this;
        }


        public ICourseQuery FilterByKeyword(string Keyword)
        {
            if (string.IsNullOrWhiteSpace(Keyword))
                return this;

            var KEYWORD = Keyword.ToUpper();
            Query = Query.Where(c => c.Title.ToUpper()
                                            .Contains(KEYWORD) || c.User.Fullname
                                            .ToUpper().Contains(KEYWORD) || c.Description
                                            .ToUpper().Contains(KEYWORD));
            return this;
        }


        public ICourseQuery IncludeCategory()
        {
            Query.Include(c => c.Category).Load();
            return this;
        }
        public ICourseQuery IncludeLectureAttachment()
        {
            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Lectures)
                 .ThenInclude(l => l.Attachments)
                 .Load();

            return this;
        }

        public ICourseQuery IncludeSection()
        {
            Query.Include(c => c.Sections.OrderBy(s => s.CreatedAt))
                 .ThenInclude(s => s.Lectures)
                 .Load();

            return this;
        }
        public ICourseQuery IncludeLectureCompletion(Guid? userId)
        {
            if (userId == null)
                return this;

            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Lectures)
                 .ThenInclude(l => l.LectureCompletion.Where(l => l.UserId == userId))
                 .Load();

            return this;
        }

        public ICourseQuery IncludeAssignment()
        {
            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Assignments)
                 .ThenInclude(a => a.Attachments)
                 .Load();

            return this;
        }

        public ICourseQuery IncludeAssignmentCompletion(Guid? userId)
        {
            if (userId == null)
                return this;

            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Assignments)
                 .ThenInclude(a => a.AssignmentCompletion.Where(a => a.UserId == userId))
                 .Load();

            return this;
        }

        public ICourseQuery IncludeQuiz()
        {
            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Quizzes)
                 .ThenInclude(q => q.Questions)
                 .ThenInclude(qu => qu.Options)
                 .Load();
            return this;
        }

        public ICourseQuery IncludeQuizCompletion(Guid? userId)
        {
            if (userId == null)
                return this;

            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Quizzes)
                 .ThenInclude(q => q.QuizCompletion.Where(q => q.UserId == userId))
                 .Load();

            return this;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Courses>> GetAllByUserIdAsync(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId
                                        && type.IsActive == true);

            return await Query.OrderByDescending(x => x.CreatedAt)
                              .ToListAsync()
                              .ConfigureAwait(false);
        }

        public Task<Courses> GetById(Guid Id)
        {
            Query = Query.Where(type => type.Id == Id);
            return Query.FirstOrDefaultAsync();
        }
        public ICourseQuery FilterStatus(Status? status)
        {
            if (status == null)
                return this;

            Query = Query.Where(type => type.status == status);
            return this;
        }
    }
}
