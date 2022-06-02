using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class SubscriptionRepository : Repository<Subscription, Guid>, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<bool> IsSubscription(Subscription subscription)
        {
            if (await FindByCondition(l => l.UserId == subscription.UserId && l.Id == subscription.Id).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            return true;
        }
        public async Task<int> GetTotal(Guid userId)
        {
            return await GetAll().Where(s => s.UserId == userId).CountAsync();
        }
    }
}
