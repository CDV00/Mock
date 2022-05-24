using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class CloseCaptionRepository : Repository<CloseCaption>, ICloseCaptionRepository
    {
        public CloseCaptionRepository(AppDbContext context): base(context)
        {

        }
        public override void Remove(CloseCaption _object)
        {
            _object.IsDeleted = true;
        }
    }
}
