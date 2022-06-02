using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class AudioLanguageRepository : Repository<AudioLanguage, Guid>, IAudioLanguageRepository
    {
        private AppDbContext _context;
        public AudioLanguageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
