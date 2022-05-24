using System;
using System.Collections.Generic;

namespace Course.BLL.Responsesnamespace
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }

        public ICollection<CategoryResponse> SubCategories { get; set; }
    }
}
