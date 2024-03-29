﻿using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Queries.Abstraction
{
    public interface IShoppingCartQuery : IQuery<ShoppingCart>
    {
        IShoppingCartQuery FilterByUserId(Guid userId);
        IShoppingCartQuery IncludeCategory();
        IShoppingCartQuery IncludeCourse();
        IShoppingCartQuery IncludeUser();
        IShoppingCartQuery IncludeDiscount();
        IShoppingCartQuery FilterByCourseId(Guid courseId);
        IShoppingCartQuery FilterByCourseIds(List<Guid> courseIds);
        Task<ShoppingCart> GetByCourseId(Guid courseId);
    }
}