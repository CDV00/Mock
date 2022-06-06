using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<SubscriptionDTO>> Add(Guid userId, Guid SubscripberId)
        {
            try
            {
                var subscription = new Subscription()
                {
                    UserId = userId,
                    SubscriberId = SubscripberId,
                };

                await _subscriptionRepository.CreateAsync(subscription);
                await _unitOfWork.SaveChangesAsync();
                return new Response<SubscriptionDTO>(
                    true, _mapper.Map<SubscriptionDTO>(subscription));
            }
            catch (Exception ex)
            {
                return new Response<SubscriptionDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Delete(Guid userId, Guid subscripberId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                           .FilterByUserId(userId)
                                                           .FilterBySubscriberId(subscripberId)
                                                           .AsSelectorAsync(s => s);
                if (subscription is null)
                {
                    return new BaseResponse(false, null, "can't find subscription");
                }
                _subscriptionRepository.Remove(subscription);

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotal(Guid userId)
        {
            try
            {
                var courses = await _subscriptionRepository.BuildQuery()
                                                           .FilterByUserId(userId)
                                                           .CountAsync();

                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<SubscriptionDTO>> IsSubscription(Guid userId, Guid subscriberId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                           .FilterByUserId(userId)
                                                           .FilterBySubscriberId(subscriberId)
                                                           .AsSelectorAsync(s => _mapper.Map<SubscriptionDTO>(s));

                return new Response<SubscriptionDTO>(true, subscription);
            }
            catch (Exception ex)
            {
                return new Response<SubscriptionDTO>(false, ex.Message, null);
            }
        }
    }
}
