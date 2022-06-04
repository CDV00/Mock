using Course.BLL.DTO;
using System;

namespace Course.DAL.DTOs
{
    public class MyCoursesDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Sale { get; set; }
        public int Parts { get; set; }
        public CategoryDTO Category { get; set; }
        public bool IsActive { get; set; }
    }
}
