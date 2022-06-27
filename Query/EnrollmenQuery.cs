using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using Query.Abstraction;
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


        public ISubscriptionQuery FilterByUserId(Guid UserId)
        {
            Query = Query.Where(type => type.UserId == UserId);
            return this;
        }

        public ISubscriptionQuery FilterByUserByKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return this;
            Query = Query.Where(type => type.User.Fullname.Contains(keyword));
            return this;
        }


        //public ISubscriptionQuery FilterByRole(Guid UserId)
        //{
        //    Query.Where(type => type.User);
        //    return this;
        //}

        public ISubscriptionQuery IncludeSubcriber()
        {
            Query.Include(u => u.Subscriber).Load();
            return this;
        }

        public ISubscriptionQuery IncludeInstructor()
        {
            Query.Include(u => u.User).Load();
            return this;
        }

        public ISubscriptionQuery FilterBySubscriberId(Guid subscriberId)
        {
            Query = Query.Where(type => type.SubscriberId == subscriberId);
            return this;
        }

       
        public ISubscriptionQuery FilterBySubscriber(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return this;
            Query = Query.Where(type => type.Subscriber.Fullname.Contains(keyword));
            return this;
        }

        public ISubscriptionQuery IncludeUser()
        {
            Query.Include(c => c.User).Load();
            return this;
        }


        public ISubscriptionQuery FilterByRole(string RoleName)
        {
            var roles = _dbContext.Roles.AsQueryable().Where(r => r.NormalizedName == RoleName.ToUpper());
            var userRole = _dbContext.UserRoles.AsQueryable().Where(ur => roles.Any(r => r.Id == ur.RoleId));

            Query = Query.Where(u => userRole.Any(ur => ur.UserId == u.User.Id));

            return this;
        }

        public ISubscriptionQuery SortByTotalSubscription()
        {

            Query = Query.OrderBy(s => s.UserId);

            return this;
        }
    }
}
