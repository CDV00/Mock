using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class OrderQuery : QueryBase<Order>, IOrderQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public OrderQuery(IQueryable<Order> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }



        /// <summary>
        /// FilterIsActive
        /// </summary>
        /// <param name="isActice"></param>
        /// <returns></returns>
        public IOrderQuery FilterIsActive(bool? isActice)
        {
            Query = Query.Where(type => isActice == null || type.IsActive == isActice);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public IOrderQuery FilterByCourseId(Guid CourseId)
        {
            Query = Query.Where(type => type.OrderItem.Any(o => o.Course.Id == CourseId));
            return this;
        }


        public IOrderQuery FilterByUserId(Guid userId)
        {

            Query = Query.Where(type => type.UserId == userId);
            return this;
        }
        public IOrderQuery IncludeCourse()
        {
            Query.Include(c => c.OrderItem).ThenInclude(o=>o.Course).Load();
            return this;
        }
        public IOrderQuery IncludeUser()
        {
            Query.Include(c => c.User).Load();
            return this;
        }
        public IOrderQuery IncludeDiscount()
        {
            Query.Include(o => o.OrderItem).ThenInclude(or => or.Course).ThenInclude(c => c.Discounts).Load();
            return this;
        }
        public IOrderQuery FilterById(Guid Id)
        {
            Query = Query.Where(type => type.Id == Id);
            return this;
        }
        public IOrderQuery GroupByCreateAt()
        {
            Query.GroupBy(type => type.CreatedAt.Date).Select(g => new
            {
                CreatedAt = g.Key
            });
            return this;
        }
        public IOrderQuery FilterByCreateAt(DateTime CreatedAt)
        {
            Query = Query.Where(type => type.CreatedAt.Date == CreatedAt.Date);
            return this;
        }
        public IOrderQuery FilterStartDate(DateTime? CreateAt)
        {
            if(CreateAt is null)
            {
                return this;
            }
            Query = Query.Where(type => type.CreatedAt >= CreateAt);
            return this;
        }
        public IOrderQuery FilterEndtDate(DateTime? CreateAt)
        {
            if (CreateAt is null)
            {
                return this;
            }
            Query = Query.Where(type => type.CreatedAt <= CreateAt);
            return this;
        }
        public IOrderQuery FilterByUserIdInstructor(Guid userId)
        {
            var orderItems = _dbContext.OrderItems.AsQueryable().Where(orderItem => orderItem.Course.UserId == userId);
            Query = Query.Where(o => orderItems.Any(oi => oi.OrderId == o.Id));

            return this;
        }
    }
}
