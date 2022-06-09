using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class SavedCoursesQuery : QueryBase<SavedCourses>, ISavedCoursesQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public SavedCoursesQuery(IQueryable<SavedCourses> savedCoursesQuery, AppDbContext dbContext) : base(savedCoursesQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        public ISavedCoursesQuery FilterByUserId(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId);
            return this;
        }

        public ISavedCoursesQuery IncludeUser()
        {
            Query.Include(type => type.Course.User).Load();
            return this;
        }

        public ISavedCoursesQuery IncludeCategory()
        {
            Query.Include(type => type.User.Category).Load();
            return this;
        }

        public ISavedCoursesQuery IncludeCourse()
        {
            Query.Include(type => type.User.Courses).Load();
            return this;
        }
        public ISavedCoursesQuery IncludeDiscount()
        {
            Query.Include(type => type.Course.Discounts).Load();
            Query.Where(Type => Type.Course.Discounts.Any(d => d.EndDate > DateTime.Now));
            return this;
        }

    }
}
