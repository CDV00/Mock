using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;
using Stripe;
using System;
using System.Threading.Tasks;

namespace MOCK_Course.BLL.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDipositRepository _dipositRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(UserManager<AppUser> userManager,
                              IDipositRepository dipositRepository,
                              IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _dipositRepository = dipositRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<PaymentDTO>> Deposit(string userId, Payment payment)
        {
            var result = await PayAsync(payment);
            if (!result.IsSuccess)
            {
                return new Response<PaymentDTO>(false, result.Message, result.StatusCode);
            }

            var user = await _userManager.FindByIdAsync(userId);
            user.Balance += payment.value.GetValueOrDefault();
            await _userManager.UpdateAsync(user);

            var diposite = new Deposit
            {
                UserId = Guid.Parse(userId),
                Amount = payment.value.GetValueOrDefault()
            };
            await _dipositRepository.CreateAsync(diposite);
            await _unitOfWork.SaveChangesAsync();

            var paymentDTO = new PaymentDTO { AddedValue = payment.value.GetValueOrDefault() };
            return new Response<PaymentDTO>(true, paymentDTO);
        }

        public async Task<BaseResponse> PayAsync(Payment payment)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51LA55GFiny4f5ziJDVSRjWn3IU4JtQh0f354rmiSuui6EM1pyHx9MpjLpqdsMX0n7ve6lTBSC6jjK2tV0jjn7mcu00GwPd1RrF";

                var optionsToken = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Name = payment.fullName,
                        Number = payment.cardNumber,
                        ExpMonth = payment.month,
                        ExpYear = payment.year,
                        Cvc = payment.cvc
                    }
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(optionsToken);

                var options = new ChargeCreateOptions
                {
                    Amount = payment.value * 100,
                    Currency = "USD",
                    Description = "Get Buy Course",
                    Source = stripeToken.Id
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (!charge.Paid)
                {
                    return new BaseResponse(false);
                }

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, null, ex.Message.ToString());
            }
        }
    }
}
