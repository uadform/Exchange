using Domain;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.WebAPI.Controllers
{
    /// <summary>
    /// Manages currency rate operations.
    /// </summary>
    [ApiController]
    [Route("currencyRates")]
    public class CurrencyRateController : ControllerBase
    {
        private readonly ICurrencyRateService _service;

        public CurrencyRateController(ICurrencyRateService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves the currency rate changes for a specific date.
        /// </summary>
        /// <param name="date">The date for which to retrieve currency rate changes.</param>
        /// <returns>A list of currency rate changes.</returns>
        /// <response code="200">Returns the list of currency rate changes.</response>
        /// <response code="204">No content available for the first day of the year.</response>
        /// <response code="400">If the date is invalid or later than December 31, 2014.</response>
        /// <response code="404">If no data is available for the specified date.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("changes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CurrencyRateChangeResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCurrencyRateChanges([FromQuery] DateTime date)
        {
            var rates = await _service.GetCurrencyRatesWithChangesAsync(date);
            return Ok(rates);
        }

        /// <summary>
        /// Retrieves the currency rate for a specific currency code.
        /// </summary>
        /// <param name="code">The currency code to retrieve the rate for.</param>
        /// <returns>The currency rate for the specified code.</returns>
        /// <response code="200">Returns the currency rate for the specified code.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurrencyRate))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCurrencyRateByCode(string code)
        {
            var rate = await _service.GetCurrencyRateByCodeAsync(code) ;
            if (rate == null) return NotFound();
            return Ok(rate);
        }
    }

}
