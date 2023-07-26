using Bandit.ACS.Client.Models;

namespace Bandit.ACQ.Daemon.Commands
{
    public class TransactionAnalyticsCommand
    {
        public string Type { get; set; } = nameof(TransactionAnalyticsCommand);
        public AnalyticsTransaction Transaction { get; set; }
    }
}
