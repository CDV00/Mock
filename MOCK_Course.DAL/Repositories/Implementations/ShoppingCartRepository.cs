using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class ShoppingCartRepository : Repository<ShoppingCart, Guid>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext context): base(context)
        {

        }
    }
}
