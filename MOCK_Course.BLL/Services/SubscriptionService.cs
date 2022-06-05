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
        //public async Task<Response<int>> GetTotal(Guid userId)
        //{
        //    try
        //    {
        //        var courses = await _subscriptionRepository.BuildQuery().FilterByOwnUserId(userId).CountAsync();
        //        return new Response<int>(true, courses);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<int>(false, ex.Message, null);
        //    }
        //}
    }
}
