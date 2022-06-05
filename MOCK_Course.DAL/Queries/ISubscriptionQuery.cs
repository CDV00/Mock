using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Queries
{
    public interface ISubscriptionQuery : IQuery<Subscription>
    {
        ISubscriptionQuery FilterByOwnUserId(Guid UserId);
        ISubscriptionQuery IncludeUser();
    }
}