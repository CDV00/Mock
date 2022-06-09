using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ICloseCaptionRepository : IRepository<CloseCaption>
    {
        //Task<bool> RemoveAll(Guid courseId);
    }
}
