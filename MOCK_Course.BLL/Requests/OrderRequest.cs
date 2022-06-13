using Course.BLL.DTO;
using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class OrderRequest
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<Guid> CartIds { get; set; }

        public Payment Payment { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public enum PaymentType
    {
        Stripe = 1,
        BalanceUser = 2
    }
    public class OrderUpdateRequest : OrderRequest
    {
    }
}