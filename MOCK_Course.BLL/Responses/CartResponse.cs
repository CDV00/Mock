using Course.DAL.Models;
using System;

namespace Course.BLL.Responses
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public CartUser CartUser { get; set; }
        public CartCourse Course { get; set; }
    }
    public class CartUser
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class CartCourse
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse category {get;set;}
    }
}
