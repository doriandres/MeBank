using MeBank.Models.Concrete;

namespace MeBank.Models
{
    public class PaymentItem
    {
        public Payment Payment { get; set; }
        public string Account { get; set; }
        public string Currency { get; set; }
        public string Service { get; set; }
    }
}