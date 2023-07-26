namespace Bandit.ACS.Daemon.Configuration
{
    public class AnalyticsConfiguration
    {
        public bool Activated { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public string ServerCertificate { get; set; }
    }
}
