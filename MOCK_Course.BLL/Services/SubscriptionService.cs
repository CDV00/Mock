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

        public async Task<Response<int>> GetTotalSubscriptions(Guid userId)
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

        public async Task<Response<int>> GetTotalSubscription(Guid subscriberId)
        {
            var total = await _subscriptionRepository.BuildQuery()
                                                     .FilterBySubscriberId(subscriberId)
                                                     .CountAsync();

            return new Response<int>(true, total);
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


        public async Task<Responses<UserDTO>> GetUserSubscription(Guid userId)
        {
            try
            {
                var user = await _subscriptionRepository.BuildQuery()
                                                        .FilterByUserId(userId)
                                                        .IncludeSubcriber()
                                                        .ToListAsync(u => _mapper.Map<UserDTO>(u.User));

                return new Responses<UserDTO>(true, user);
            }
            catch (Exception ex)
            {
                return new Responses<UserDTO>(false, ex.Message, null);
            }
        }
        //
        //
        public async Task<Responses<UserDTO>> GetPopularInstructor()
        {
            try
            {
                string RoleName = UserRoles.Instructor;
                var user = await _subscriptionRepository.BuildQuery()
                                                        .IncludeSubcriber()
                                                        .IncludeUser()
                                                        //.FilterByRole(RoleName)
                                                        .ToListAsync(u => _mapper.Map<UserDTO>(u.User));

                return new Responses<UserDTO>(true, user);
            }
            catch (Exception ex)
            {
                return new Responses<UserDTO>(false, ex.Message, null);
            }
        }
    }
}
