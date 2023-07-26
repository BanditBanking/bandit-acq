using Bandit.ACS.Client.Models;
using Bandit.ACS.Daemon.Models.DTOs;

namespace Bandit.ACS.Daemon.Services
{
    public interface IAnalyticsService
    {
        Task<TransactionAnalyticsResultDTO> SendTransactionAsync(AnalyticsTransaction transaction);
    }
}
