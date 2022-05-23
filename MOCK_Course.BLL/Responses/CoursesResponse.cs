using System;
using Course.DAL.Models;

namespace Course.BLL.Responses
{
    public class CoursesResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }

    }
}
