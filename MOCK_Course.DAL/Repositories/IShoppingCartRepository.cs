using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart,Guid>
    {
    }
}
