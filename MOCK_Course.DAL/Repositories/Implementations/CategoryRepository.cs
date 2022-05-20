using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(Category _object)
        {
            _object.IsDeleted = true;
        }
    }
}
