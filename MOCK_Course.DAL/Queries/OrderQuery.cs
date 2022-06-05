using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
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

            Query = Query.Where(type => type.CourseId == CourseId);
            return this;
        }


        public IOrderQuery FilterByUserId(Guid userId)
        {

            Query = Query.Where(type => type.UserId == userId);
            return this;
        }
    }
}
