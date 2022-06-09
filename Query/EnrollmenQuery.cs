﻿using Course.DAL.Data;
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


        public ISubscriptionQuery FilterByUserId(Guid UserId)
        {
            Query.Where(type => type.UserId == UserId);
            return this;
        }

        public ISubscriptionQuery IncludeSubcriber()
        {
            Query.Include(u => u.Subscriber).Load();
            return this;
        }

        public ISubscriptionQuery FilterBySubscriberId(Guid subscriberId)
        {
            Query.Where(type => type.SubscriberId == subscriberId);
            return this;
        }

        public ISubscriptionQuery IncludeUser()
        {
            Query.Include(c => c.User).Load();
            return this;
        }
    }
}