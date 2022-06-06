using System;
using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.BLL.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface ISubscriptionService
    {
        Task<Response<SubscriptionDTO>> Add(Guid userId, Guid SubscripberId);
        Task<BaseResponse> Delete(Guid userId, Guid subscripberId);
        Task<Response<int>> GetTotal(Guid userId);
        Task<Response<SubscriptionDTO>> IsSubscription(Guid userId, Guid subscriberId);
        Task GetTotalInstrutorsSubscribing(Guid userId);
    }
}
