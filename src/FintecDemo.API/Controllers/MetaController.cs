using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FintecDemo.API.Controllers
{
    [ApiController]
    public class MetaController: ControllerBase
    {
        /// <summary>
        /// Provides a message that tells user where to find the API docs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetApiRootAsync()
        {
            return Ok(new
            {
                Message = "Check docs at /docs for more information"
            });
        }

        /// <summary>
        /// Can be invoked from hosting environment to determine API is up and running
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("meta/is-ready")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> IsReadyAsync()
        {
            return Ok();
        }

        /// <summary>
        /// Can be invoked from hosting environment to determine API is still alive and healthy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("meta/is-healthy")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> IsHealthyAsync()
        {
            return Ok();
        }
    }
}
