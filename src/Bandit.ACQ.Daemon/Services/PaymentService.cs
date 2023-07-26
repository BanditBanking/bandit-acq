using Bandit.ACQ.Daemon.Configuration;
using Bandit.ACQ.Daemon.Exceptions;
using Bandit.ACQ.Daemon.Helpers;
using Bandit.ACQ.Daemon.Models;
using Bandit.ACS.Client;
using Bandit.ACS.Client.Models;
using System.Net.Sockets;

namespace Bandit.ACQ.Daemon.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICertificateHelper _certificateHelper;
        private readonly DaemonConfiguration _config;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ILogger<PaymentService> logger, ICertificateHelper certificateHelper, DaemonConfiguration config)
        {
            _logger = logger;
            _certificateHelper = certificateHelper;
            _config = config;
        }

        public async Task<AnalyticsTransaction> RequestPaymentAsync(Bank bank, string cardNumber, string paymentToken)
        {
            try
            {
                var serverCertificate = await _certificateHelper.LoadCertificateAsync(bank.Certificate);
                using var acsClient = new AcsTcpClient(bank.Ip, bank.Port, serverCertificate);
                var result = await acsClient.RequestPayment(_config.BankNet.BankId, cardNumber, paymentToken);
                if (result.IsSuccess == false)
                {
                    _logger.LogError($"Invalid payment request");
                    throw new InvalidPaymentRequestException(bank.Id, paymentToken);
                }
                return result.Transaction;
            } catch (SocketException)
            {
                throw new BankUnreachableException(bank.Id, bank.Ip, bank.Port);
            }
        }
    }
}
