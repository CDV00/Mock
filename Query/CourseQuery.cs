﻿using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            Query.Where(c => c.Orders.Any(o => o.UserId == userId));
            return this;
        }

        /// <summary>
        /// FilterByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ICourseQuery FilterByUserId(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId);
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

        public ICourseQuery IncludeUser()
        {
            Query.Include(c => c.User).Load();
            return this;
        }

        public ICourseQuery IncludeLevel()
        {
            Query.Include(c => c.Levels).Load();
            return this;
        }

        public ICourseQuery IncludeLanguage()
        {
            Query.Include(c => c.AudioLanguages)
                 .Include(c => c.CloseCaptions)
                 .Load();
            return this;
        }

        public ICourseQuery FilterByCategoryId(Guid? categoryId)
        {
            if (categoryId == null)
                return this;

            Query = Query.Where(c => c.CategoryId == categoryId);
            return this;
        }


        public ICourseQuery FilterByDiscount(bool? IsSeller)
        {
            if (IsSeller == null || IsSeller == false)
                return this;

            Query = Query.Where(c => c.Discounts.Any(d=>d.EndDate > DateTime.Now));
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

        //public ICourseQuery orderBy(string orderBy)
        //{
        //    if (orderBy == null)
        //        return this;

        //    switch (orderBy)
        //    {
        //        case "date":
        //            Query = Query.OrderBy(c => c.CreatedBy);
        //            break;
        //        case "subscription":
        //            Query = Query.OrderBy(c => c.Enrollments.Count());
        //            break;
        //    }

        //    return this;
        //}


        public ICourseQuery FilterByKeyword(string Keyword)
        {
            if (string.IsNullOrWhiteSpace(Keyword))
                return this;

            var KEYWORD = Keyword.ToUpper();
            Query = Query.Where(c => c.Title.ToUpper().Contains(KEYWORD) || c.User.Fullname
                                            .ToUpper().Contains(KEYWORD) || c.Description
                                            .ToUpper().Contains(KEYWORD));
            return this;
        }


        public ICourseQuery IncludeCategory()
        {
            Query.Include(c => c.Category).Load();
            return this;
        }

        public ICourseQuery IncludeSection()
        {
            Query.Include(c => c.Sections).ThenInclude(s => s.Lectures).Load();
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
            return await Query.OrderByDescending(x => x.CreatedAt).ToListAsync().ConfigureAwait(false);
        }

        public Task<Courses> GetById(Guid Id)
        {
            Query = Query.Where(type => type.Id == Id);
            return Query.FirstOrDefaultAsync();
        }
    }
}