using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class AudioLanguageRepository : Repository<AudioLanguage>, IAudioLanguageRepository
    {
        public AudioLanguageRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(AudioLanguage _object)
        {
            _object.IsDeleted = true;
        }
    }
}
