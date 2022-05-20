using Course.DAL.Data;
using Course.DAL.Models;

namespace Course.DAL.Repositories.Implementations
{
    public class SectionRepositoty : Repository<Section>, ISectionRepositoty
    {
        public SectionRepositoty(AppDbContext context): base(context)
        {

        }
        public override void Remove(Section _object)
        {
            _object.IsDeleted = true;
        }
    }
}
