using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class DiscountRepository : Repository<Discount, Guid>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext context): base(context)
        {

        }
    }
}
