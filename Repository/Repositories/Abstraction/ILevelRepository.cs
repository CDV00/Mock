using Course.DAL.DTOs;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface ILevelRepository : IRepository<Level>
    {
        Task<bool> CheckExists(List<Guid> ids);
        Task<IList<LevelDTO>> GetAll();
    }
}
