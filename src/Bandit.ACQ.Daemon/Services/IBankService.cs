using Bandit.ACQ.Daemon.Models;

namespace Bandit.ACQ.Daemon.Services
{
    public interface IBankService
    {
        Task<Bank> GetBankAsync(string bankIdentifier);
    }
}
