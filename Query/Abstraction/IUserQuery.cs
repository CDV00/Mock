using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;

namespace Query.Abstraction
{
    public interface IUserQuery : IQuery<AppUser>
    {
        IUserQuery FilterByName(string keyword);
        IUserQuery FilterByRole(string RoleName);
        IUserQuery SortBySubscription(bool IsPupular);
    }
}