using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.DTOs;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface IPaymentService
    {
        Task<Response<PaymentDTO>> Deposit(string userId, Payment payment);
        Task<BaseResponse> PayAsync(Payment payment);
    }
}