using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class UserQuery : QueryBase<AppUser>, IUserQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public UserQuery(IQueryable<AppUser> userQuery, AppDbContext dbContext) : base(userQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }



        public IUserQuery FilterByRole(string RoleName)
        {
            var roles = _dbContext.Roles.AsQueryable().Where(r => r.NormalizedName == RoleName.ToUpper());
            var userRole = _dbContext.UserRoles.AsQueryable().Where(ur => roles.Any(r => r.Id == ur.RoleId));

            Query = Query.Where(u => userRole.Any(ur => ur.UserId == u.Id));

            return this;
        }

        public IUserQuery SortBySubscription(bool IsPopular)
        {
            if (!IsPopular)
                return this;

            Query = Query.OrderByDescending(u => u.Subscriptions.Count());
            return this;
        }

        public IUserQuery FilterByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return this;

            Query = Query.Where(u => u.Fullname.ToUpper().Contains(keyword.ToUpper()));

            return this;
        }
    }
}
