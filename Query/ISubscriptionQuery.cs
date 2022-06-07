using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Queries
{
    public interface ISubscriptionQuery : IQuery<Subscription>
    {
        ISubscriptionQuery FilterBySubscriberId(Guid subscriberId);
        ISubscriptionQuery FilterByUserId(Guid UserId);
        ISubscriptionQuery IncludeUser();
        ISubscriptionQuery IncludeSubcriber();
    }
}