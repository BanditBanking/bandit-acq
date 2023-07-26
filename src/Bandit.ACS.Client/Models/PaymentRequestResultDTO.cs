namespace Bandit.ACS.Client.Models
{
    public class PaymentRequestResultDTO
    {
        public bool IsSuccess { get; set; }

        public AnalyticsTransaction Transaction { get; set; }
    }
}
