using Course.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class UpcommingCourseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public CourseCategoryDTO Category { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
    }
}
