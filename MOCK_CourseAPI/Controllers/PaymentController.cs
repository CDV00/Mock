using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCK_Course.BLL.Services;
using MOCK_Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOCK_Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Route("pay")]
        [HttpPost]
        public async Task<dynamic> Payment(Payment payment)
        {
            return await _paymentService.PayAsync(payment);
        }
    }
}
