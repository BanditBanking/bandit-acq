using Bandit.ACQ.Daemon.Models.DTOs;
using Bandit.ACS.Daemon.Exceptions;

namespace Bandit.ACQ.Daemon.Exceptions
{
    [Serializable]
    public class InvalidPaymentRequestException : Exception, IExposedException
    {
        private string _bankId;
        private string _paymentToken;

        public InvalidPaymentRequestException() { }

        public InvalidPaymentRequestException(string bankId, string paymentToken) : base($"No transaction could be completed at {bankId} using token {paymentToken}")
        {
            _bankId = bankId;
            _paymentToken = paymentToken;
        }

        public ProblemDetailDTO Expose() => new()
        {
            HttpCode = StatusCodes.Status422UnprocessableEntity,
            ErrorCode = "woodstock",
            Title = "Invalid payment request",
            Detail = $"No transaction could be completed at {_bankId} using token {_paymentToken}"
        };
    }
}
