using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class AudioLanguageRepository : Repository<AudioLanguage, Guid>, IAudioLanguageRepository
    {
        private AppDbContext _context;
        public AudioLanguageRepository(AppDbContext context): base(context)
        {
            _context = context;
        }
        public override void Remove(AudioLanguage _object)
        {
            if (_object == null)
                return;
            _context.AudioLanguages.Remove(_object);
        }
    }
}
