using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(ShoppingCart _object)
        {
            _object.IsDeleted = true;
        }
    }
}
