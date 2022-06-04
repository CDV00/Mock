using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface ISectionQuery : IQuery<Section>
    {
        ISectionQuery FilterByCourseId(Guid Id);
    }
}
