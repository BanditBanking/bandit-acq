using Bandit.ACQ.Clients;
using Bandit.ACQ.Daemon.Configuration;
using Bandit.ACQ.Daemon.Helpers;
using Bandit.ACS.Client.Models;
using Bandit.ACS.Daemon.Models.DTOs;
using Bandit.ACS.Daemon.Services;

namespace Bandit.ACQ.Daemon.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ILogger<AnalyticsService> _logger;
        private readonly ICertificateHelper _certificateHelper;
        private readonly DaemonConfiguration _config;

        public AnalyticsService(ILogger<AnalyticsService> logger, ICertificateHelper certificateHelper, DaemonConfiguration config)
        {
            _logger = logger;
            _certificateHelper = certificateHelper;
            _config = config;
        }

        public async Task<TransactionAnalyticsResultDTO> SendTransactionAsync(AnalyticsTransaction transaction)
        {
            var serverCertificate = await _certificateHelper.LoadCertificateAsync(_config.Analytics.ServerCertificate);
            using var analyticsClient = new AnalyticsSslClient(_config.Analytics.ServerAddress, _config.Analytics.ServerPort, serverCertificate);
            var result = await analyticsClient.SyncTransactionAsync(transaction);
            if(!result.IsSuccess) _logger.LogError($"Transaction sync with analytics server failure");
            return result;
        }
    }
}
