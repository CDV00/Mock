using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICloseCaptionRepository : IRepository<CloseCaption, Guid>
    {
        Task<bool> RemoveAll(Guid courseId);
    }
}
