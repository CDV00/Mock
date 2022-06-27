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
    public class NotificationQuery : QueryBase<Notification>, INotificationQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public NotificationQuery(IQueryable<Notification> notificationQuery, AppDbContext dbContext) : base(notificationQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }
        //public INotificationQuery FilterByKeyword(string Keyword)
        //{
        //    if (string.IsNullOrWhiteSpace(Keyword))
        //        return this;

        //    var KEYWORD = Keyword.ToUpper();
        //    Query = Query.Where(c => c.Header.ToUpper()
        //                                    .Contains(KEYWORD) || c.Body
        //                                    .ToUpper().Contains(KEYWORD));
        //    return this;
        //}
        //public INotificationQuery IncludeToUser()
        //{
        //    Query.Include(c => c.ToUser)
        //         .Load();
        //    return this;
        //}
        //public INotificationQuery IncludeFromUser()
        //{
        //    Query.Include(c => c.FromUser)
        //         .Load();
        //    return this;
        //}

    }
}
