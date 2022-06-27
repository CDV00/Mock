using Course.BLL.DTO;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Abstraction
{
    public interface IUserRepository : IRepository<AppUser>
    {
        IUserQuery BuildQuery();
        Task<PagedList<UserDTO>> GetAllUserByRole(UserParameter parameter);
        
    }
}
