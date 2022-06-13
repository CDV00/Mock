using Course.BLL.Requests;
using System.Threading.Tasks;

namespace MOCK_Course.BLL.Services.Implementations
{
    public interface IPaymentService
    {
        Task<dynamic> PayAsync(Payment payment);
    }
}