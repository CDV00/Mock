using System;
using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface ISubscriptionService
    {
        Task<BaseResponse> Add(Guid userId, SubscriptionRequest subscriptionRequest);
        Task<Response<int>> GetTotal(Guid userId);
    }
}
