using Bandit.ACQ.Daemon.Configuration;
using Bandit.ACQ.Daemon.Models.DTOs;
using Bandit.ACQ.Daemon.Services;
using Bandit.ACS.Daemon.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bandit.ACQ.Daemon.Controllers
{
    [ApiController]
    [Route("payments")]
    [Produces("application/json")]
    public class PaymentsControllers : ControllerBase
    {
        private readonly DaemonConfiguration _config;
        private readonly ILogger<PaymentsControllers> _logger;
        private readonly IPaymentService _paymentService;
        private readonly IBankService _bankService;
        private readonly IAnalyticsService _analyticsService;

        public PaymentsControllers(DaemonConfiguration config, ILogger<PaymentsControllers> logger, IPaymentService paymentService, IBankService bankService, IAnalyticsService analyticsService)
        {
            _config = config;
            _logger = logger;
            _paymentService = paymentService;
            _bankService = bankService;
            _analyticsService = analyticsService;
        }

        /// <summary>
        /// Request a payment (PRQ)
        /// </summary>
        /// <remarks>
        /// For the moment, this endpoint doesn't require any form of authentication
        /// </remarks>
        /// <param name="paymentRequestDTO">Object containing the bank identifier and the payment token</param>
        /// <returns>Returns an ACK or a NACK depending on the validity of the provided token</returns>
        /// <response code="200">Indicates that the token was correct, the transaction will be registered directly to the NBS</response>
        /// <response code="404">If no bank was found with the provided id. Documentation available at: https://github.com/TristesseLOL/bandit-acq/blob/master/documentation/errors.md#salamander</response>
        /// <response code="422">If the provided token could not be associated to a request. Documentation available at: https://github.com/TristesseLOL/bandit-acs/blob/master/documentation/errors.md#woodstock </response>
        /// <response code="503">If the bank server is unreachabale. Documentation available at: https://github.com/TristesseLOL/bandit-acs/blob/master/documentation/errors.md#harbor</response>
        [HttpPost("/request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetailDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetailDTO), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetailDTO), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> PaymentRequest([FromBody] PaymentRequestDTO paymentRequestDTO)
        {
            var bank = await _bankService.GetBankAsync(paymentRequestDTO.BankIdentifier);
            var transaction = await _paymentService.RequestPaymentAsync(bank, paymentRequestDTO.CardNumber, paymentRequestDTO.PaymentToken);
            _logger.LogDebug($"Retrieved transaction {transaction.Id}");

            if(_config.Analytics.Activated)
            {
                _logger.LogDebug($"Sending transaction {transaction.Id} to analytics");
                await _analyticsService.SendTransactionAsync(transaction);
            }
            return Ok(); 
        }
    }
}
