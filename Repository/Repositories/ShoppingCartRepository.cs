using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IShoppingCartQuery BuildQuery()
        {
            return new ShoppingCartQuery(_context.Carts.AsQueryable(), _context);
        }
        public async Task<bool> checkPrice(Guid courseId)
        {
            decimal price = await BuildQuery().FilterByCourseId(courseId)
                                            .AsSelectorAsync(s=>s.Course.Price);
            if (price <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
