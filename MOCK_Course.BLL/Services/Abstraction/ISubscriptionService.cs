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
    }
}
