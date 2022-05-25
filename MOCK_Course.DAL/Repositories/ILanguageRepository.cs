using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories
{
    public interface ILanguageRepository : IRepository<Language, Guid>
    {
    }
}