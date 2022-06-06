using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
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
