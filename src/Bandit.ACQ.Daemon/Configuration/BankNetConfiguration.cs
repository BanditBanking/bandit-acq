using Bandit.ACQ.Daemon.Models;

namespace Bandit.ACQ.Daemon.Configuration
{
    public class BankNetConfiguration
    {
        public string ConfigPath { get; set; } = "/config/banknet.json";
        public string BankId { get; set; }
        public Bank[] Banks { get; set; }
    }
}
