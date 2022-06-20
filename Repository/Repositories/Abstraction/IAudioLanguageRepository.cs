using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IAudioLanguageRepository : IRepository<AudioLanguage>
    {
        //Task<bool> RemoveAll(Guid courseId);
        Task<bool> CheckExists(List<Guid> ids);
    }
}
