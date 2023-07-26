using System.Security.Cryptography.X509Certificates;

namespace Bandit.ACQ.Daemon.Helpers
{
    public class LocalCertificateHelper : ICertificateHelper
    {
        private ILogger<LocalCertificateHelper> _logger;

        public LocalCertificateHelper(ILogger<LocalCertificateHelper> logger)
        {
            _logger = logger;
        }

        public async Task<X509Certificate2> LoadCertificateAsync(string filePath)
        {
            try
            {
                byte[] certBytes = await File.ReadAllBytesAsync(filePath);
                return new X509Certificate2(certBytes);
            }
            catch (FileNotFoundException)
            {
                _logger.LogCritical($"Certificate not found : {filePath}");
                throw;
            }
        }
    }
}
