using Course.DAL.Models;
using Query.Abstraction;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        ISubscriptionQuery BuildQuery();
    }
}
