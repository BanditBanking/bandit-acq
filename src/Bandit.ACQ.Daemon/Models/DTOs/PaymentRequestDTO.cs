namespace Bandit.ACQ.Daemon.Models.DTOs
{
    public class PaymentRequestDTO
    {
        public string BankIdentifier { get; set; }
        public string PaymentToken { get; set; }
        public string CardNumber { get; set; }
    }
}
