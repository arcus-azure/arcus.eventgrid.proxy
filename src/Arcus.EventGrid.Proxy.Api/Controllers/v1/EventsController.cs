using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Arcus.EventGrid.Proxy.Api.Controllers.v1
{
    [Route("api/v1/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        /// <summary>
        ///     Emit Event
        /// </summary>
        /// <remarks>Sends an event to Azure Event Grid Topic</remarks>
        /// <param name="eventType">Type of the event</param>
        /// <param name="eventPayload">Event payload</param>
        /// <param name="eventId">Id of the event</param>
        /// <param name="eventSubject">Subject of the event</param>
        /// <param name="eventTimestamp">Timestamp of the event</param>
        /// <param name="dataVersion">Data version</param>
        [Route("{eventType}")]
        [HttpPost]
        [SwaggerOperation(OperationId = "Events_Emit")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Event was successfully emitted")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unable to emit event due to internal error")]
        public IActionResult Emit(string eventType, [FromBody, Required] object eventPayload, string eventId, string eventSubject, string eventTimestamp, string dataVersion)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }
    }
}
