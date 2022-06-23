using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public ICategoryQuery BuildQuery()
        {
            return new CategoryQuery(_context.Categories.AsQueryable(), _context);
        }

        public async Task<bool> Existing(Guid? id)
        {
            if (id == null)
                return true;
            return await BuildQuery().FilterById(id.GetValueOrDefault()).AnyAsync();
        }
    }
}
