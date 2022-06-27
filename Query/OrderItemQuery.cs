using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using Query.Abstraction;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public class OrderItemQuery : QueryBase<OrderItem>, IOrderItemQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public OrderItemQuery(IQueryable<OrderItem> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }
        public IOrderItemQuery FilterByCourseId(Guid CourseId)
        {
            Query = Query.Where(type => type.CourseId == CourseId);
            return this;
        }
        public IOrderItemQuery FilterByUserId(Guid userId)
        {
            Query = Query.Where(type => type.Order.UserId == userId);
            return this;
        }
        public IOrderItemQuery IncludeCourse()
        {
            Query.Include(o => o.Course).Load();
            return this;
        }
        public IOrderItemQuery FilterStartDate(DateTime? CreateAt)
        {
            if (CreateAt is null)
            {
                return this;
            }
            Query = Query.Where(type => type.Order.CreatedAt >= CreateAt);
            return this;
        }
        public IOrderItemQuery FilterEndtDate(DateTime? CreateAt)
        {
            if (CreateAt is null)
            {
                return this;
            }
            Query = Query.Where(type => type.Order.CreatedAt <= CreateAt);
            return this;
        }
        public IOrderItemQuery FilterByUserIdInstructor(Guid userId)
        {
            Query = Query.Where(o => o.Course.UserId == userId);

            return this;
        }
    }
}
