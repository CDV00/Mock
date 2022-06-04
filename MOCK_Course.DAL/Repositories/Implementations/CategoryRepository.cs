using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
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

    }
}
