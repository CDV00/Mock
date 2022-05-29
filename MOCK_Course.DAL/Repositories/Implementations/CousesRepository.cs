using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CousesRepository : Repository<Courses, Guid>, ICousesRepository
    {
        private AppDbContext _context;
        public CousesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Courses>> GetAllForCard()
        {

            return await GetAll().Include(c => c.Category).Include(c => c.User).ToListAsync();
        }

        public async Task<Courses> GetForPost(Guid id)
        {
            return await FindByCondition(c => c.Id == id).Include(c => c.AudioLanguages).Include(c => c.CloseCaptions).FirstOrDefaultAsync();
        }
    }
}
