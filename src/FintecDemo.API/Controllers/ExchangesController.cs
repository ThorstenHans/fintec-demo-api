using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FintecDemo.API.Models;
using FintecDemo.API.Models.Stock;
using FintecDemo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FintecDemo.API.Controllers
{
    [Route("api/exchanges")]
    public class ExchangesController : Controller
    {
        private readonly IExchangeService _service;

        public ExchangesController(IExchangeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new exchange
        /// </summary>
        /// <param name="exchange">The exchange that should be created</param>
        /// <remarks>
        /// This API may return a HTTP status code `400` - in this case a exchange with the same shortcut is already existing
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ExchangeDetailsModel>> CreateExchangeAsync([FromBody] ExchangeCreateModel exchange)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdExchange = await _service.CreateAsync(exchange);
                return new ObjectResult(createdExchange) {StatusCode = 201};
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Returns a list of all exchanges 
        /// </summary>
        /// <remarks>
        /// This API will return all available exchanges in an unordered list.        
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IEnumerable<ExchangeListModel>>> GetAllExchangesAsync()
        {
            var allExchanges = await _service.GetAllAsync();
            if (!allExchanges.Any())
            {
                return NoContent();
            }

            return Ok(allExchanges);
        }

        /// <summary>
        /// Retrieve a exchange by it's shortcut
        /// </summary>
        /// <param name="shortcut">Shortcut of the exchange you're looking for</param>
        [HttpGet]
        [Route("{shortcut:alpha:required}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ExchangeDetailsModel>> GetExchangeByShortcutAsync(string shortcut)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
            {
                return BadRequest();
            }

            var found = await _service.GetByShortcutAsync(shortcut);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [HttpGet]
        [Route("{shortcut:alpha:required}/stocks")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<StockListModel>>> GetStocksTradedAtGivenExchangeAsync(string shortcut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(shortcut))
            {
                return BadRequest();
            }

            var allStocks = await _service.GetAllStocksTradedAtExchangeAsync(shortcut);
            if (allStocks == null)
            {
                return NotFound();
            }

            if (!allStocks.Any())
            {
                return NoContent();
            }

            return Ok(allStocks);
        }

        /// <summary>
        /// Update an existing exchange
        /// </summary>
        /// <param name="shortcut">The exchange's shortcut using for identification</param>
        /// <param name="exchange">The modifications as an instance of <see cref="ExchangeUpdateModel"/></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{shortcut:alpha:required}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<ExchangeDetailsModel>> UpdateSTockAsync(string shortcut, [FromBody] ExchangeUpdateModel exchange)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(shortcut) || exchange == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedExchange = await _service.UpdateAsync(shortcut, exchange);
                if (updatedExchange == null)
                {
                    return NotFound();
                }

                return Ok(updatedExchange);
            }
            catch (DBConcurrencyException)
            {
                return Conflict();
            }
        }

        /// <summary>
        /// Update a exchange partially using JSON Patch expressions
        /// </summary>
        /// <param name="shortcut">The exchange's shortcut using for identification</param>
        /// <param name="exchangePatch">The modifications as JSON Patch</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{shortcut:alpha:required}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<ExchangeDetailsModel>> PatchExchangeAsync(string shortcut,
            [FromBody] JsonPatchDocument<ExchangeUpdateModel> exchangePatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(shortcut))
            {
                return BadRequest();
            }

            try
            {
                var updated = await _service.PatchAsync(shortcut, exchangePatch);
                if (updated == null)
                {
                    return NotFound();
                }

                return Ok(updated);
            }
            catch (JsonPatchException jsonPatchException)
            {
                return BadRequest(jsonPatchException.Message);
            }
            catch (DBConcurrencyException)
            {
                return Conflict();
            }
        }

        /// <summary>
        /// Delete an existing exchange by it's shortcut
        /// </summary>
        /// <param name="shortcut">The exchange's shortcut that should be deleted</param>
        [HttpDelete]
        [Route("{shortcut:alpha:required}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteExchangeAsync(string shortcut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(shortcut))
            {
                return BadRequest();
            }

            if (!await _service.DeleteExchangeAsync(shortcut))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
