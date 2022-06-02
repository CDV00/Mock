using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription, Guid>
    {
        Task<bool> IsSubscription(Subscription subscriptionRequest);
        Task<int> GetTotal(Guid userId);
    }
}
