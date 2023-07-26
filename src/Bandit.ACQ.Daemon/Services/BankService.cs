using Bandit.ACQ.Daemon.Configuration;
using Bandit.ACQ.Daemon.Exceptions;
using Bandit.ACQ.Daemon.Models;

namespace Bandit.ACQ.Daemon.Services
{
    public class BankService : IBankService
    {
        private readonly ILogger<BankService> _logger;
        private readonly DaemonConfiguration _config;

        public BankService(ILogger<BankService> logger, DaemonConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<Bank> GetBankAsync(string bankIdentifier)
        {
            var bank = _config.BankNet.Banks.Where(b => b.Id == bankIdentifier).FirstOrDefault();
            if (bank == null)
            {
                _logger.LogError($"Bank \"{bankIdentifier}\" not found");
                throw new BankNotFoundException(bankIdentifier);
            }
            return await Task.FromResult(bank);
        }
    }
}
