using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Arcus.EventGrid.Contracts;
using Arcus.EventGrid.Parsers;
using Arcus.EventGrid.Proxy.Api;
using Arcus.EventGrid.Proxy.Tests.Integration.Services;
using Arcus.EventGrid.Testing.Infrastructure.Hosts.ServiceBus;
using Arcus.EventGrid.Testing.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Arcus.EventGrid.Proxy.Tests.Integration.Endpoints.v1
{
    [Collection("Integration")]
    public class EventEndpointTests : EndpointTest, IAsyncLifetime
    {
        private ServiceBusEventConsumerHost _serviceBusEventConsumerHost;
        private readonly EventService _eventService;

        public EventEndpointTests()
        {
            _eventService = new EventService(Configuration);
        }

        public async Task InitializeAsync()
        {
            var connectionString = Configuration.GetValue<string>("Arcus:ServiceBus:ConnectionString");
            var topicName = Configuration.GetValue<string>("Arcus:ServiceBus:TopicName");

            var serviceBusEventConsumerHostOptions = new ServiceBusEventConsumerHostOptions(topicName, connectionString);
            _serviceBusEventConsumerHost = await ServiceBusEventConsumerHost.StartAsync(serviceBusEventConsumerHostOptions, new NoOpLogger());
        }

        public async Task DisposeAsync()
        {
            await _serviceBusEventConsumerHost.StopAsync();
        }

        [Fact]
        public async Task Emit_BasicCallWithDefaults_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var eventPayload = new { LicensePlate = Guid.NewGuid().ToString() };
            string rawEventPayload = JsonConvert.SerializeObject(eventPayload);
            JToken expectedEventPayload = JToken.Parse(rawEventPayload);

            // Act
            using (HttpResponseMessage response = await _eventService.EmitAsync(rawEventPayload, eventType))
            {
                // Assert
                string content = await response.Content.ReadAsStringAsync();
                Assert.True(HttpStatusCode.NoContent == response.StatusCode, content);
                
                string eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
                string eventSubject = AssertHttpHeader(Headers.Response.Events.Subject, response);
                string eventTimestamp = AssertHttpHeader(Headers.Response.Events.Timestamp, response);
                AssertReceivedEvent(eventId, eventType, eventTimestamp, eventSubject, expectedEventPayload);
            }
        }

        private void AssertReceivedEvent(string eventId, string eventType, string eventTimestamp, string eventSubject, JToken expectedEventPayload)
        {
            Event receivedEvent = GetReceivedEvent(eventId);
            DateTimeOffset parsedTime = DateTimeOffset.Parse(eventTimestamp);
            Assert.Equal(parsedTime.DateTime, receivedEvent.EventTime);
            Assert.Equal(eventType, receivedEvent.EventType);
            Assert.Equal(eventId, receivedEvent.Id);
            Assert.Equal(eventSubject, receivedEvent.Subject);
            Assert.Equal(expectedEventPayload.ToString(Formatting.None), receivedEvent.Data);
        }

        [Fact]
        public async Task Emit_BasicCallWithEventId_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var expectedEventId = Guid.NewGuid().ToString();
            var eventPayload = new { LicensePlate = Guid.NewGuid().ToString() };
            string rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            using (HttpResponseMessage response = await _eventService.EmitAsync(rawEventPayload, eventType, eventId: expectedEventId))
            {
                string content = await response.Content.ReadAsStringAsync();
                Assert.True(HttpStatusCode.NoContent == response.StatusCode, content);

                string eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
                Event receivedEvent = GetReceivedEvent(eventId);
                Assert.NotNull(receivedEvent);
                Assert.Equal(expectedEventId, receivedEvent.Id);
            }
        }

        [Fact]
        public async Task Emit_BasicCallWithEventSubject_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var licensePlate = Guid.NewGuid().ToString();
            string expectedEventSubject = $"/cars/{licensePlate}";
            var eventPayload = new { LicensePlate = licensePlate };
            string rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            using (HttpResponseMessage response = await _eventService.EmitAsync(rawEventPayload, eventType, eventSubject: expectedEventSubject))
            {
                // Assert
                string content = await response.Content.ReadAsStringAsync();
                Assert.True(HttpStatusCode.NoContent == response.StatusCode, content);
            
                string eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
                Event receivedEvent = GetReceivedEvent(eventId);
                Assert.NotNull(receivedEvent);
                Assert.Equal(expectedEventSubject, receivedEvent.Subject);
            }
        }

        [Fact]
        public async Task Emit_BasicCallWithEventTime_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            DateTimeOffset expectedTimestamp = DateTimeOffset.UtcNow;
            var eventPayload = new { LicensePlate = Guid.NewGuid().ToString() };
            string rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            using (HttpResponseMessage response = await _eventService.EmitAsync(rawEventPayload, eventType, eventTimestamp: expectedTimestamp.ToString("O")))
            {
                // Assert
                string content = await response.Content.ReadAsStringAsync();
                Assert.True(HttpStatusCode.NoContent == response.StatusCode, content);

                string eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
                Event receivedEvent = GetReceivedEvent(eventId);
                Assert.NotNull(receivedEvent);
                Assert.Equal(expectedTimestamp.DateTime, receivedEvent.EventTime);
            }
        }

        [Fact]
        public async Task Emit_BasicCallWithDataVersion_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var licensePlate = Guid.NewGuid().ToString();
            var eventPayload = new { LicensePlate = licensePlate };
            string rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            using (HttpResponseMessage response = await _eventService.EmitAsync(rawEventPayload, eventType))
            {
                // Assert
                string content = await response.Content.ReadAsStringAsync();
                Assert.True(HttpStatusCode.NoContent == response.StatusCode, content);

                string eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
                Event receivedEvent = GetReceivedEvent(eventId);
                Assert.NotNull(receivedEvent);
            }
        }

        private Event GetReceivedEvent(string eventId)
        {
            string receivedEvent = _serviceBusEventConsumerHost.GetReceivedEvent(eventId);
            EventBatch<Event> rawEvents = EventParser.Parse(receivedEvent);
            Assert.NotNull(rawEvents);
            Assert.NotNull(rawEvents.Events);
            
            Event firstEvent = Assert.Single(rawEvents.Events);
            Assert.NotNull(firstEvent);

            return firstEvent;
        }

        private static string AssertHttpHeader(string headerName, HttpResponseMessage response)
        {
            bool isHeaderFound = response.Headers.TryGetValues(headerName, out IEnumerable<string> headerValues);
            Assert.True(isHeaderFound, $"Cannot find response header '{headerName} in HTTP response'");
            
            string headerValue = Assert.Single(headerValues);
            Assert.False(String.IsNullOrEmpty(headerValue), $"HTTP response header '{headerName}' value is empty");

            return headerValue;
        }
    }
}