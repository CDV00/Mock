using Course.DAL.Data;
using Course.DAL.Models;
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
            Query = Query.Where(c => c.Orders.Any(o => o.UserId == userId));
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
            Query = Query.Where(type => isActice == null || type.IsActive == isActice);
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
            Query.Include(c => c.Discounts)
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

        //public ICourseQuery FilterByUserId(Guid? userId)
        //{
        //    if (userId == null)
        //        return this;

        //    Query = Query.Where(c => c.UserId == userId);
        //    return this;
        //}


        public ICourseQuery IncludeCategory()
        {
            Query.Include(c => c.Category).Load();
            return this;
        }

        public ICourseQuery IncludeSection()
        {
            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Lectures)
                 .Include(c=>c.Sections)
                 .ThenInclude(s=>s.Assignments)
                 .Include(c => c.Sections)
                 .ThenInclude(s => s.Quizzes)
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
        public ICourseQuery IncludeQuiz()
        {
            Query.Include(c => c.Sections)
                 .ThenInclude(s => s.Quizzes)
                 .ThenInclude(q => q.Questions)
                 .ThenInclude(qu => qu.Options)
                 .Include(c => c.Sections)
                 .ThenInclude(s => s.Quizzes)
                 .ThenInclude(q => q.Settings)
                 .Load();
            return this;
        }

        public ICourseQuery IncludeOrder()
        {
            Query.Include(c => c.Orders)
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
    }
}
