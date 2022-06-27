using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface ICategoryQuery : IQuery<Category>
    {
        ICategoryQuery FilterById(Guid id);
        ICategoryQuery FilterByParent(Guid? id);
        ICategoryQuery FilterBySubCategory();
        ICategoryQuery FilterIsActive(bool? isActice);
        ICategoryQuery FilterTopCategory();
        ICategoryQuery IncludeSubCategory();
    }
}