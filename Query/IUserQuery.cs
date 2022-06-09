﻿using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;

namespace Course.DAL.Queries
{
    public interface IUserQuery : IQuery<AppUser>
    {
    }
}