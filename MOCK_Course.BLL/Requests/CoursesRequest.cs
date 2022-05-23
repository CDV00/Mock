using System;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class CoursesRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }

    }
}
