using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Responses;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Course.BLL.Share.RequestFeatures;

namespace Course.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Response<SubscriptionDTO>> Add(Guid userId, Guid instructorId)
        {
            try
            {
                var IsExits = await _subscriptionRepository.BuildQuery()
                                                           .FilterByUserId(instructorId)
                                                           .FilterBySubscriberId(userId)
                                                           .AnyAsync();

                if (IsExits)
                    return new Response<SubscriptionDTO>(false, "You are already subscription", null);

                var subscription = new Subscription()
                {
                    UserId = instructorId,
                    SubscriberId = userId,
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

        public async Task<BaseResponse> Delete(Guid userId, Guid instructorId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                                .FilterByUserId(instructorId)
                                                                .FilterBySubscriberId(userId)
                                                                .AsSelectorAsync(s => s);

                if (subscription is null)
                {
                    return new BaseResponse(false, null, "can't find subscription");
                }
                _subscriptionRepository.Remove(subscription, true);

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotalSubscriber(Guid userId)
        {
            try
            {
                var total = await _subscriptionRepository.BuildQuery()
                                                         .FilterByUserId(userId)
                                                         .CountAsync();

                return new Response<int>(true, total);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotalInstructor(Guid subscriberId)
        {
            var total = await _subscriptionRepository.BuildQuery()
                                                     .FilterBySubscriberId(subscriberId)
                                                     .CountAsync();

            return new Response<int>(true, total);
        }

        public async Task<Response<SubscriptionDTO>> IsSubscription(Guid userId, Guid instructorId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                                .FilterByUserId(instructorId)
                                                                .FilterBySubscriberId(userId)
                                                                .AsSelectorAsync(s => _mapper.Map<SubscriptionDTO>(s));

                return new Response<SubscriptionDTO>(true, subscription);
            }
            catch (Exception ex)
            {
                return new Response<SubscriptionDTO>(false, ex.Message, null);
            }
        }



        public async Task<PagedList<UserDTO>> GetAllSubscriber(Guid userId, SubscriptionParameters subscriptionParameters)
        {
            var user = await _subscriptionRepository.BuildQuery()
                                                    .FilterByUserId(userId)
                                                    .FilterByUserByKeyword(subscriptionParameters.Keyword)
                                                    .IncludeSubcriber()
                                                    .ToListAsync(u => _mapper.Map<UserDTO>(u.Subscriber));

            var count = await _subscriptionRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .FilterByUserByKeyword(subscriptionParameters.Keyword)
                                                     .CountAsync();

            return new PagedList<UserDTO>(user, count, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
        }

        public async Task<PagedList<UserDTO>> GetAllInstructor(SubscriptionParameters subscriptionParameters, Guid userId)
        {
            var user = await _subscriptionRepository.BuildQuery()
                                                    .FilterBySubscriberId(userId)
                                                    .FilterBySubscriber(subscriptionParameters.Keyword)
                                                    .IncludeInstructor()
                                                    .ToListAsync(u => _mapper.Map<UserDTO>(u.User));

            var count = await _subscriptionRepository.BuildQuery()
                                                     .FilterBySubscriberId(userId)
                                                     .FilterBySubscriber(subscriptionParameters.Keyword)
                                                     .CountAsync();


            return new PagedList<UserDTO>(user, count, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
        }
        public async Task<Response<SubscriptionDTO>> IsSubscribed(Guid userId, Guid instructorId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                                .FilterByUserId(instructorId)
                                                                .FilterBySubscriberId(userId)
                                                                .AsSelectorAsync(s => _mapper.Map<SubscriptionDTO>(s));

                if (subscription == null)
                {
                    return new Response<SubscriptionDTO>(false, subscription);
                }

                return new Response<SubscriptionDTO>(true, subscription);
            }
            catch (Exception ex)
            {
                return new Response<SubscriptionDTO>(false, ex.Message, null);
            }
        }
    }

}
