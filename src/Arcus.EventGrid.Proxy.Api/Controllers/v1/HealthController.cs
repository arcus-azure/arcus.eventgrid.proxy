using System.Net;
using System.Threading.Tasks;
using GuardNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;

namespace Arcus.EventGrid.Proxy.Api.Controllers.v1
{
    [Route("api/v1/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthController"/> class.
        /// </summary>
        /// <param name="healthCheckService">The service to provide the health of the API application.</param>
        public HealthController(HealthCheckService healthCheckService)
        {
            Guard.NotNull(healthCheckService, nameof(healthCheckService));

            _healthCheckService = healthCheckService;
        }

        /// <summary>
        ///     Get Health
        /// </summary>
        /// <remarks>Provides an indication about the health of the runtime</remarks>
        [HttpGet]
        [SwaggerOperation(OperationId = "Health_Get")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Runtime is up and running in a healthy state", Type = typeof(HealthReport))]
        [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, Description = "Runtime is not healthy")]
        public async Task<IActionResult> Get()
        {
            HealthReport healthReport = await _healthCheckService.CheckHealthAsync();

            return healthReport?.Status == HealthStatus.Healthy ? Ok(healthReport) : StatusCode(StatusCodes.Status503ServiceUnavailable, healthReport);
        }
    }
}