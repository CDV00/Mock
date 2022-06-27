using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface INotificationQuery : IQuery<Notification>
    {
        //INotificationQuery FilterByKeyword(string Keyword);
        //INotificationQuery IncludeFromUser();
        //INotificationQuery IncludeToUser();
    }
}