using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System;
using System.Threading.Tasks;

namespace MOCK_Course.BLL.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly UserManager<AppUser> _userManager;
        public PaymentService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<PaymentDTO> Deposit(string userId, Payment payment)
        {
            var result = await PayAsync(payment);
            if (!result.Equals("Success"))
            {
                return new PaymentDTO
                {
                    IsSuccess = false,
                    Message = result
                };
            }
            var user = await _userManager.FindByIdAsync(userId);
            user.Balance += payment.value;
            await _userManager.UpdateAsync(user);
            return new PaymentDTO()
            {
                AddedValue = payment.value,
                IsSuccess = true,
                Message = result
            };

        }

        public async Task<BaseResponse> PayAsync(Payment payment)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51L5pVeEbAgWO9RQtII0iYh6WFl9Zl4F30LOXH7xU3E0UoyBnlQhpuRQMSqy6GjNbdGZVOtII5WNXCybSfKqP74lb001fUsK8WB";

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
                    return new BaseResponse(true);
                }

                return new BaseResponse(false);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, null, ex.Message.ToString());
            }
        }
    }
}
