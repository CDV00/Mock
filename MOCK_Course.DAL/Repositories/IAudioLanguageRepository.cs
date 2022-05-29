using Course.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface IAudioLanguageRepository : IRepository<AudioLanguage, Guid>
    {
        Task<bool> RemoveAll(Guid courseId);
    }
}
