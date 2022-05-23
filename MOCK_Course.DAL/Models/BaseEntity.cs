using System;

namespace Course.DAL.Models
{
    public partial class BaseEntity<T>
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public BaseEntity(T id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
