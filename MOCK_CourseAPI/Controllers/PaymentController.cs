using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.DAL.DTOs;
using CourseAPI.Extensions.ControllerBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MOCK_Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> Deposit(Payment payment)
        {
            var userId = User.GetUserId();
            var result = await _paymentService.Deposit(userId.ToString(), payment);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
