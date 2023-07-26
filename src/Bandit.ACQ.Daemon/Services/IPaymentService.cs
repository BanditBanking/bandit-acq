using Bandit.ACQ.Daemon.Models;
using Bandit.ACS.Client.Models;

namespace Bandit.ACQ.Daemon.Services
{
    public interface IPaymentService
    {
        Task<AnalyticsTransaction> RequestPaymentAsync(Bank bank, string cardNumber, string paymentToken);
    }
}
