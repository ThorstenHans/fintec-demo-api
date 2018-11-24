using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FintecDemo.API.Models.CompanyProfile;
using FintecDemo.API.Models.Stock;
using FintecDemo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FintecDemo.API.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StocksController: ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }
        
        /// <summary>
        /// Retrieve a list containing all stocks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<StockListModel>> GetStocksAsync()
        {
            var allStocks = await _stockService.GetAllAsync();
            if (!allStocks.Any())
            {
                return NoContent();
            }

            return Ok(allStocks);
        }

        /// <summary>
        /// Get detailed information about a single stock
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{isin:alpha:required}")]
        public async Task<ActionResult<StockDetailsModel>> GetStockByIsinAsync(string isin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(isin))
            {
                return BadRequest();
            }

            var stock = await _stockService.GetByIsinAsync(isin);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        /// <summary>
        /// Get detailed information about the company
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{isin:alpha:required}/companyprofile")]
        public async Task<ActionResult<CompanyProfileDetailsModel>> GetCompanyProfileByIsinAsync(string isin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(isin))
            {
                return BadRequest();
            }

            var companyProfile = await _stockService.GetCompanyProfileByIsinAsync(isin);
            if (companyProfile == null)
            {
                return NotFound();
            }

            return Ok(companyProfile);
        }

        /// <summary>
        /// Retrieve a list of exchanges currently trading a given stock
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{isin:alpha:required}/exchanges")]
        public async Task<ActionResult<IEnumerable<string>>> GetExchangesTradingGivenStockAsync(string isin)
        {
            return Ok(new List<string>());
        }

        /// <summary>
        /// Create a new stock
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<StockDetailsModel>> CreateStockAsync([FromBody] StockCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createModel == null)
            {
                return BadRequest();
            }

            try
            {
                var createdStock = await _stockService.CreateStockAsync(createModel);
                return StatusCode(201, createdStock);
            }
            catch (ArgumentException)
            {
                return Conflict();
            }
        }

        /// <summary>
        /// Delete a stock by it's ISIN
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{isin:alpha:required}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStockByIsinAsync(string isin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(isin))
            {
                return BadRequest();
            }

            if (!await _stockService.DeleteStockByIsinAsync(isin))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Update an existing stock
        /// </summary>
        /// <param name="isin">The stock's shortcut using for identification</param>
        /// <param name="updateModel">The modifications as an instance of <see cref="StockUpdateModel"/></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{isin:alpha:required}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public  ActionResult<StockDetailsModel> UpdateStockByIsinAsync(string isin, [FromBody] StockUpdateModel updateModel)
        {
            //TODO: implement this async
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(isin) || updateModel == null)
            {
                return BadRequest();
            }

            return Ok();
        }
        /// <summary>
        /// Update a stock partially using JSON Patch expressions
        /// </summary>
        /// <param name="isin">The stock's isin using for identification</param>
        /// <param name="stockPatch">The modifications as JSON Patch</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{isin:alpha:required}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ActionResult<StockDetailsModel> PatchStockByIsinAsync(string isin, JsonPatchDocument<StockUpdateModel> stockPatch)
        {
            //TODO: implement this async
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(isin) || stockPatch == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
