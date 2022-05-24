using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(Language _object)
        {
            _object.IsDeleted = true;
        }
    }
}
