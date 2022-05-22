using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Category : BaseEntity<Guid>
    {
        public Category()
        {
        }

        public Category(Guid Id, string name, Guid? parentId) : base(Id)
        {
            Name = name;
            ParentId = parentId;
        }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Courses> Courses { get; set; }
        public ICollection<AppUser> User { get; set; }
    }
}
