﻿using System;
using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Entities.DTOs;
using Entities.ParameterRequest;
using Entities.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface ISubscriptionService
    {
        Task<Response<SubscriptionDTO>> Add(Guid userId, Guid instructorId);
        Task<BaseResponse> Delete(Guid userId, Guid subscripberId);
        Task<Response<int>> GetTotalSubscriber(Guid userId);
        Task<Response<SubscriptionDTO>> IsSubscription(Guid userId, Guid subscriberId);
        Task<PagedList<UserDTO>> GetAllSubscriber(Guid userId, SubscriptionParameters subscriptionParameters);
        Task<Response<int>> GetTotalInstructor(Guid subscriberId);
        Task<PagedList<UserDTO>> GetAllInstructor(SubscriptionParameters subscriptionParameters, Guid userId);
        Task<Response<SubscriptionDTO>> IsSubscribed(Guid userId, Guid instructorId);
        Task<ApiOkResponse<ListAnalysisSubscriberResponse>> CountSubcriberByMonth(Guid subscriberId);
    }
}
