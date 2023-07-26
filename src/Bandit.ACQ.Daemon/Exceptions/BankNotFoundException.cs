using Bandit.ACQ.Daemon.Models.DTOs;
using Bandit.ACS.Daemon.Exceptions;

namespace Bandit.ACQ.Daemon.Exceptions
{
    [Serializable]
    public class BankNotFoundException : Exception, IExposedException
    {
        private string _bankId;

        public BankNotFoundException() { }

        public BankNotFoundException(string bankId) : base($"Could not find a bank with id {bankId}")
        {
            _bankId = bankId;
        }

        public ProblemDetailDTO Expose() => new()
        {
            HttpCode = StatusCodes.Status404NotFound,
            ErrorCode = "salamander",
            Title = "Bank not found",
            Detail = $"Could not find a bank with id {_bankId}"
        };
    }
}
