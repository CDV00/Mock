using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Course.DAL.Queries.Abstraction
{
    public interface IShoppingCartQuery : IQuery<ShoppingCart>
    {
        IShoppingCartQuery FilterByUserId(Guid userId);
        IShoppingCartQuery IncludeCategory();
        IShoppingCartQuery IncludeCourse();
        IShoppingCartQuery IncludeUser();
        IShoppingCartQuery IncludeDiscount();
        IShoppingCartQuery FilterByIds(List<Guid> ids);
    }
}