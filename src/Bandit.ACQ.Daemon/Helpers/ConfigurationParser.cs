using Bandit.ACQ.Daemon.Configuration;
using Bandit.ACQ.Daemon.Models;
using System.Text.Json;

namespace Bandit.ACQ.Daemon.Helpers
{
    public class ConfigurationParser
    {
        public static DaemonConfiguration Parse(IConfiguration config, bool IsDevelopment)
        {
            var parsedConfig = config.GetSection(DaemonConfiguration.ServiceName).Get<DaemonConfiguration>() ?? new DaemonConfiguration();
            try
            {
                var banks = JsonSerializer.Deserialize<List<Bank>>(File.ReadAllText(parsedConfig.BankNet.ConfigPath));
                parsedConfig.BankNet.Banks = JsonSerializer.Deserialize<Bank[]>(File.ReadAllText(parsedConfig.BankNet.ConfigPath)) ?? Array.Empty<Bank>();
                //if (IsDevelopment)
                //    parsedConfig.BankNet.Banks = parsedConfig.BankNet.Banks.Select(b => { b.Ip = parsedConfig.BankNet.DevelopmentIp; return b;}).ToArray();
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to read banknet config located at : {parsedConfig.BankNet.ConfigPath}");
            }
            return parsedConfig;
        }
    }
}
