using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Query.Abstraction
{
    public interface ISubscriptionQuery : IQuery<Subscription>
    {
        ISubscriptionQuery FilterBySubscriberId(Guid subscriberId);
        ISubscriptionQuery FilterByUserId(Guid UserId);
        ISubscriptionQuery IncludeUser();
        ISubscriptionQuery IncludeSubcriber();
        ISubscriptionQuery FilterByRole(string RoleName);
        ISubscriptionQuery IncludeInstructor();

        /// <summary>
        /// filter by full name
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        ISubscriptionQuery FilterBySubscriber(string keyword);
        ISubscriptionQuery FilterByUserByKeyword(string keyword);
    }
}