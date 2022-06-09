using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Query.Abstraction
{
    public interface IUserQuery : IQuery<AppUser>
    {
        IUserQuery FilterByRole(string RoleName);
    }
}
