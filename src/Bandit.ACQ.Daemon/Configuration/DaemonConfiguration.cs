using Bandit.ACS.Daemon.Configuration;

namespace Bandit.ACQ.Daemon.Configuration
{
    public class DaemonConfiguration
    {
        public const string ServiceName = "ACQ";
        public TCPConfiguration TCP { get; set; }
        public SSLConfiguration SSL { get; set; }
        public APIConfiguration API { get; set; }
        public BankNetConfiguration BankNet { get; set; }
        public AnalyticsConfiguration Analytics { get; set; }

    }
}
