namespace Course.BLL.Requests
{
    public class Payment
    {
        public string fullName { get; set; }
        public string cardNumber { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string cvc { get; set; }
        public int value { get; set; }
    }
}
