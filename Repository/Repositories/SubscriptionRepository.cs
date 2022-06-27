using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Query.Abstraction;
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
    }
}
