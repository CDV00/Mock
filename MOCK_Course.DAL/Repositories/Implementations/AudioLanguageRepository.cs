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
        public override void Remove(AudioLanguage _object)
        {
            if (_object == null)
                return;
            _context.AudioLanguages.Remove(_object);
        }

        public async Task<bool> RemoveAll(Guid courseId)
        {
            var audioLanguages = await GetAll().Where(a => a.CourseId == courseId).ToListAsync();

            if (audioLanguages.Count == 0)
                return false;

            foreach (var item in audioLanguages)
            {
                Remove(item);
            }

            return true;
        }
    }
}
