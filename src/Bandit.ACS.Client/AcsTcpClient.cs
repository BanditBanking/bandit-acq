using Bandit.ACS.Client.Helpers;
using Bandit.ACS.Client.Models;
using Bandit.ACS.Commands;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Bandit.ACS.Client
{
    public class AcsTcpClient : IDisposable
    {
        private readonly SslStream _sslStream;
        private readonly TcpClient _client;
        private readonly X509Certificate2 _serverCertificate;

        public AcsTcpClient(string host, int port, X509Certificate2 serverCertificate)
        {
            _client = new TcpClient(host, port);
            _serverCertificate = serverCertificate;
            _sslStream = new SslStream(_client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            _sslStream.AuthenticateAsClient(host);
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            // TODO: Verify that the certificate match the expected server certificate
            // return sslPolicyErrors == SslPolicyErrors.None;
            return true;
        }

        public async Task<PaymentRequestResultDTO> RequestPayment(string bankId, string cardNumber, string paymentToken)
        {
            var command = new PaymentRequestCommand { BankId = bankId, CardNumber = cardNumber, PaymentToken = paymentToken };
            await TcpHelper.SendAsync(_sslStream, command);
            return await TcpHelper.ReadAsync<PaymentRequestResultDTO>(_sslStream);
        }

        public void Dispose()
        {
            _sslStream.Dispose();
            _client.Dispose();
        }
    }
}
