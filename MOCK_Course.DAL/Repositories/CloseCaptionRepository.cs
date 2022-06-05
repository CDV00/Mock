using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using System;

namespace Course.DAL.Repositories
{
    public class CloseCaptionRepository : Repository<CloseCaption, Guid>, ICloseCaptionRepository
    {
        private AppDbContext _context;
        public CloseCaptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
