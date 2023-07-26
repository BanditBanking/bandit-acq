using Bandit.ACQ.Daemon.Models.DTOs;
using Bandit.ACS.Daemon.Exceptions;

namespace Bandit.ACQ.Daemon.Exceptions
{
    [Serializable]
    public class BankUnreachableException : Exception, IExposedException
    {
        private string _bankId;
        private string _ip;
        private int _port;

        public BankUnreachableException() { }

        public BankUnreachableException(string bankId, string ip, int port) : base($"Bank \"{bankId}\" unreachable ({ip}:{port})")
        {
            _bankId = bankId;
            _ip = ip;
            _port = port;
        }

        public ProblemDetailDTO Expose() => new()
        {
            HttpCode = StatusCodes.Status503ServiceUnavailable,
            ErrorCode = "harbor",
            Title = "Bank server unreachable",
            Detail = $"Bank {_bankId} unreachable ({_ip}:{_port})"
        };
    }
}
