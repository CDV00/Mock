using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class SectionRepositoty : Repository<Section, Guid>, ISectionRepositoty
    {
        public SectionRepositoty(AppDbContext context): base(context)
        {

        }
    }
}
