using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICloseCaptionRepository : IRepository<CloseCaption>
    {
        //Task<bool> RemoveAll(Guid courseId);
        Task<bool> CheckExists(List<Guid> ids);
    }
}
