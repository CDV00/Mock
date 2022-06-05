using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class SubscriptionQuery : QueryBase<Subscription>, ISubscriptionQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public SubscriptionQuery(IQueryable<Subscription> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        public ISubscriptionQuery FilterByOwnUserId(Guid UserId)
        {
            return this;
        }

        public ISubscriptionQuery IncludeUser()
        {
            Query.Include(c => c.User).Load();
            return this;
        }
    }
}
