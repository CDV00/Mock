using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
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
            var orderIds = _dbContext.Orders.Where(o => o.UserId == userId).Select(o => o.CourseId);

            Query = Query.Where(type => orderIds.Contains(type.Id));
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

        public ICourseQuery IncludeLanguage()
        {
            Query.Include(c => c.AudioLanguages).Include(c => c.CloseCaptions).Load();
            return this;
        }

        public ICourseQuery FilterByCategoryId(Guid? categoryId)
        {
            if (categoryId == null)
                return this;

            Query = Query.Where(c => c.CategoryId == categoryId);
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

        public ICourseQuery ApplySort(string orderby)
        {
            if (string.IsNullOrWhiteSpace(orderby))
            {
                return this;
            }

            var orderParams = orderby.Trim().Split(',');
            var propertyInfos = typeof(Courses).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrEmpty(orderQuery))
            {
                return this;
            }
            return this;
        }

        public ICourseQuery FilterByKeyword(string Keyword)
        {
            Query = Query.Where(c => c.Title.ToUpper().Contains(Keyword) || c.User.Fullname
                                            .ToUpper().Contains(Keyword) || c.Description
                                            .ToUpper().Contains(Keyword));
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
