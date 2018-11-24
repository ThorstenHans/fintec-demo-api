using FintecDemo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FintecDemo.API.Controllers
{
    [ApiController]
    [Route("api/exchanges")]
    public class NotationsController : Controller
    {
        /// <summary>
        /// Get all notations traded at a given exchange
        /// </summary>
        /// <param name="shortcut"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{shortcut:alpha:required}/notations")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<object> GetAllNotationsAsync(string shortcut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(shortcut))
            {
                return BadRequest();
            }
            return Ok(null);
        }

        /// <summary>
        /// Get detailed information about a notation
        /// </summary>
        /// <param name="shortcut"></param>
        /// <param name="isin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{shortcut:alpha:required}/notations/{isin:alpha:required}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNotationByIsinAsync(string shortcut, string isin)
        {
            //TODO: implement async
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(shortcut) || string.IsNullOrWhiteSpace(isin))
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Get historic values (one week) for a notation at a given exchange
        /// </summary>
        /// <param name="shortcut"></param>
        /// <param name="isin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{shortcut:alpha:required}/notations/{isin:alpha:required}/history")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNotationHistoryByIsinAsync(string shortcut, string isin)
        {
            //TODO: implement async
            return Ok();
        }
        
        /// <summary>
        /// Get historic (custom period) values for a notation at a given exchange
        /// </summary>
        /// <param name="shortcut"></param>
        /// <param name="isin"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{shortcut:alpha:required}/notations/{isin:alpha:required}/history/{period:required}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNotationPeriodicHistoryByIsinAsync(string shortcut, string isin, HistoryPeriod period)
        {
            //TODO: implement async
            return Ok();
        }
    }
}
