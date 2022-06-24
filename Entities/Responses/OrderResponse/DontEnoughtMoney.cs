namespace Entities.Responses
{
    public class DontEnoughtMoney : ApiNotFoundResponse
    {
        public DontEnoughtMoney(decimal balance, decimal price)
               : base($"Your balance: {balance}, but total price course: {price}. You don't have enough money to payment (Nạp lần đầu đi<:)")
        {
        }
    }
}
