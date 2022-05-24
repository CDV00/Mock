using System;

namespace Course.BLL.Responses
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public CartUserResponse CartUser { get; set; }
        public CartCourseResponse Course { get; set; }
    }
    public class CartUserResponse
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
    }
    public class CartCourseResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public CartCategory category {get;set;}
    }
    public  class CartCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
