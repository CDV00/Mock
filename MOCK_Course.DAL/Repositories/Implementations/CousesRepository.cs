using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class CousesRepository : Repository<Courses, Guid>, ICousesRepository
    {
        private AppDbContext _context;
        public CousesRepository(AppDbContext context): base(context)
        {
            _context = context;
        }
    }
}
