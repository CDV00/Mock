using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public class ShoppingCartQuery : QueryBase<ShoppingCart>, IShoppingCartQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public ShoppingCartQuery(IQueryable<ShoppingCart> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        public IShoppingCartQuery FilterByUserId(Guid userId)
        {
            Query = Query.Where(type => type.UserId == userId);
            return this;
        }

        public IShoppingCartQuery FilterByCourseIds(List<Guid> courseIds)
        {
            Query = Query.Where(type => courseIds.Contains(type.CourseId));
            return this;
        }

        public IShoppingCartQuery IncludeUser()
        {
            Query.Include(type => type.Course.User).Load();
            return this;
        }

        public IShoppingCartQuery IncludeCategory()
        {
            Query.Include(type => type.Course.Category).Load();
            return this;
        }

        public IShoppingCartQuery IncludeCourse()
        {
            Query.Include(type => type.Course).Load();
            return this;
        }
        public IShoppingCartQuery IncludeDiscount()
        {
            Query.Include(type => type.Course.Discounts).Load();
            Query.Where(Type => Type.Course.Discounts.Any(d => d.EndDate > DateTime.Now));
            return this;
        }
        public IShoppingCartQuery FilterByCourseId(Guid courseId)
        {
            Query = Query.Where(type => type.Course.Id == courseId);
            return this;
        }
        public Task<ShoppingCart> GetByCourseId(Guid courseId)
        {
            Query = Query.Where(type => type.Course.Id == courseId);
            return Query.FirstOrDefaultAsync();
        }
    }
}
