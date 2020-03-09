using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Arcus.EventGrid.Publishing.Interfaces;
using CloudNative.CloudEvents;
using GuardNet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace Arcus.EventGrid.Proxy.Api.Controllers.v1
{
    [Route("api/v1/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventGridPublisher _eventGridPublisher;

        public EventsController(IEventGridPublisher eventGridPublisher)
        {
            Guard.NotNull(eventGridPublisher, nameof(eventGridPublisher));
            _eventGridPublisher = eventGridPublisher;
        }

        /// <summary>
        ///     Emit Event
        /// </summary>
        /// <remarks>Sends an event to an Azure Event Grid Topic</remarks>
        /// <param name="eventType">Type of the event</param>
        /// <param name="eventPayload">Event payload</param>
        /// <param name="eventId">Id of the event</param>
        /// <param name="eventSubject">Subject of the event</param>
        /// <param name="eventTimestamp">Timestamp of the event. Example of preferred format is '2019-01-21T10:25:09.2292418+01:00'</param>
        /// <param name="dataVersion">Version of the data payload schema</param>
        [Route("{eventType}")]
        [HttpPost]
        [SwaggerOperation(OperationId = "Events_Emit")]
        [SwaggerResponse((int) HttpStatusCode.NoContent, Description = "Event was successfully emitted")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Description = "The request to emit an event was not valid")]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Description = "Unable to emit event due to internal error")]
        public async Task<IActionResult> Emit(string eventType, [FromBody, Required] object eventPayload, string eventId, string eventTimestamp, string eventSubject = "/", string dataVersion = "1.0")
        {
            eventId = string.IsNullOrWhiteSpace(eventId) ? Guid.NewGuid().ToString() : eventId;
            eventTimestamp = string.IsNullOrWhiteSpace(eventTimestamp) ? DateTimeOffset.UtcNow.ToString(format: "O") : eventTimestamp;
            JToken rawjsonToken = JToken.FromObject(eventPayload);

            if (DateTimeOffset.TryParse(eventTimestamp, out DateTimeOffset parsedEventTimeStamp) == false)
            {
                return BadRequest($"Unable to parse specified event timestamp '{eventTimestamp}'");
            }

            var source = new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}");
            await _eventGridPublisher.PublishRawCloudEventAsync(
                CloudEventsSpecVersion.V1_0, eventId, eventType, source, rawjsonToken.ToString(Formatting.None), eventSubject, parsedEventTimeStamp.DateTime);

            Response.Headers.Add(Headers.Response.Events.Id, eventId);
            Response.Headers.Add(Headers.Response.Events.Subject, eventSubject);
            Response.Headers.Add(Headers.Response.Events.Timestamp, eventTimestamp);
            Response.Headers.Add(Headers.Response.Events.DataVersion, dataVersion);

            return NoContent();
        }
    }
}