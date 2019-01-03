using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Arcus.EventGrid.Sidecar.Api.Controllers.v1
{
    [Route("api/v1/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        ///     Get Health
        /// </summary>
        /// <remarks>Provides an indication about the health of the runtime</remarks>
        [HttpGet]
        [SwaggerOperation(OperationId = "Health_Get")]
        [SwaggerResponse((int) HttpStatusCode.OK, Description = "Runtime is up and running in a healthy state")]
        [SwaggerResponse((int) HttpStatusCode.ServiceUnavailable, Description = "Runtime is not healthy")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}