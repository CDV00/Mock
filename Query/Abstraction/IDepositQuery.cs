using Course.DAL.Queries.Abstraction;
using Entities.Models;
using System;

namespace Query.Abstraction
{
    public interface IDepositQuery : IQuery<Deposit>
    {
        IDepositQuery FilterByUser(Guid userId);
        IDepositQuery FilterDateEnd(DateTime? dateSend);
        IDepositQuery FilterDateStart(DateTime? dateStart);
    }
}