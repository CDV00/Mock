using Course.DAL.Models;
using Course.DAL.Queries;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        //Task<bool> IsSubscription(Subscription subscriptionRequest);
        //Task<int> GetTotal(Guid userId);
        ISubscriptionQuery BuildQuery();
    }
}
