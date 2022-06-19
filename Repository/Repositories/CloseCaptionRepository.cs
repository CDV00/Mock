using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CloseCaptionRepository : Repository<CloseCaption>, ICloseCaptionRepository
    {
        private AppDbContext _context;
        public CloseCaptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> CheckExists(List<Guid> ids)
        {
            var countIs = await Entity().Where(a => ids.Contains(a.Id))
                                        .CountAsync();
            return ids.Count == countIs;
        }
    }
}
