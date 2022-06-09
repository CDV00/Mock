using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private AppDbContext _context;
        public SubscriptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public ISubscriptionQuery BuildQuery()
        {
            return new SubscriptionQuery(_context.Subscriptions.AsQueryable(), _context);
        }
        //public async Task<bool> IsSubscription(Subscription subscription)
        //{
        //    if (await FindByCondition(l => l.UserId == subscription.UserId && l.Id == subscription.Id).FirstOrDefaultAsync() == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<int> GetTotal(Guid userId)
        //{
        //    return await GetAll().Where(s => s.UserId == userId).CountAsync();
        //}
    }
}
