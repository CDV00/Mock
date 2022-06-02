using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Course.BLL.DTO;

namespace Course.BLL.Services.Implementations
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

        public async Task<BaseResponse> Add(Guid userId, SubscriptionRequest subscriptionRequest)
        {
            try
            {
                var subscription = _mapper.Map<Subscription>(subscriptionRequest);

                await _subscriptionRepository.CreateAsync(subscription);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(
                    true);
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
                var courses = await _subscriptionRepository.GetTotal(userId);
                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }
    }
}
