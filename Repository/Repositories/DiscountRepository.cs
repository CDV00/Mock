using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Course.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private AppDbContext _context;
        public DiscountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IDiscountQuery BuildQuery()
        {
            return new DiscountQuery(_context.Discounts.AsQueryable(), _context);
        }

        public async Task<Discount> GetByIdAsync(Guid courseId, Guid Id)
        {
            var discount = await BuildQuery().FilterByCourseId(courseId)
                                             .FilterById(Id)
                                             .AsSelectorAsync(d => d);
            if (discount == null)
                return null;

            _context.Entry(discount).State = EntityState.Modified;

            return discount;
        }
    }
}
