using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Arcus.EventGrid.Contracts;
using Arcus.EventGrid.Parsers;
using Arcus.EventGrid.Sidecar.Api;
using Arcus.EventGrid.Sidecar.Tests.Integration.Services;
using Arcus.EventGrid.Testing.Infrastructure.Hosts.ServiceBus;
using Arcus.EventGrid.Testing.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Arcus.EventGrid.Sidecar.Tests.Integration.Endpoints.v1
{
    [Collection("Integration")]
    public class EventEndpointTests : IAsyncLifetime
    {
        private ServiceBusEventConsumerHost _serviceBusEventConsumerHost;
        private EventService _eventService = new EventService();

        public EventEndpointTests()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        protected IConfiguration Configuration { get; }

        public async Task InitializeAsync()
        {
            var connectionString = Configuration.GetValue<string>("Arcus:ServiceBus:ConnectionString");
            var topicName = Configuration.GetValue<string>("Arcus:ServiceBus:TopicName");

            var serviceBusEventConsumerHostOptions = new ServiceBusEventConsumerHostOptions(topicName, connectionString);
            _serviceBusEventConsumerHost = await ServiceBusEventConsumerHost.Start(serviceBusEventConsumerHostOptions, new NoOpLogger());
        }

        public async Task DisposeAsync()
        {
            await _serviceBusEventConsumerHost.Stop();
        }

        [Fact]
        public async Task Emit_BasicCallWithDefaults_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var eventPayload = new { LicensePlate = Guid.NewGuid().ToString() };
            var rawEventPayload = JsonConvert.SerializeObject(eventPayload);
            var expectedEventPayload = JToken.Parse(rawEventPayload);

            // Act
            var response = await _eventService.EmitAsync(rawEventPayload, eventType);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
            var eventSubject = AssertHttpHeader(Headers.Response.Events.Subject, response);
            var eventTimestamp = AssertHttpHeader(Headers.Response.Events.Timestamp, response);
            var eventDataVersion = AssertHttpHeader(Headers.Response.Events.DataVersion, response);
            AssertReceivedEvent(eventId, eventType, eventTimestamp, eventSubject, eventDataVersion, expectedEventPayload);
        }

        [Fact]
        public async Task Emit_BasicCallWithEventId_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var expectedEventId = Guid.NewGuid().ToString();
            var eventPayload = new { LicensePlate = Guid.NewGuid().ToString() };
            var rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            var response = await _eventService.EmitAsync(rawEventPayload, eventType, eventId: expectedEventId);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
            var receivedEvent = GetReceivedEvent(eventId);
            Assert.NotNull(receivedEvent);
            Assert.Equal(expectedEventId, receivedEvent.Id);
        }

        [Fact]
        public async Task Emit_BasicCallWithEventSubject_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var licensePlate = Guid.NewGuid().ToString();
            var expectedEventSubject = $"/cars/{licensePlate}";
            var eventPayload = new { LicensePlate = licensePlate };
            var rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            var response = await _eventService.EmitAsync(rawEventPayload, eventType, eventSubject: expectedEventSubject);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
            var receivedEvent = GetReceivedEvent(eventId);
            Assert.NotNull(receivedEvent);
            Assert.Equal(expectedEventSubject, receivedEvent.Subject);
        }

        [Fact]
        public async Task Emit_BasicCallWithEventTime_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var expectedTimestamp = DateTimeOffset.UtcNow;
            var eventPayload = new { LicensePlate = Guid.NewGuid().ToString() };
            var rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            var response = await _eventService.EmitAsync(rawEventPayload, eventType, eventTimestamp: expectedTimestamp.ToString("O"));

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
            var receivedEvent = GetReceivedEvent(eventId);
            Assert.NotNull(receivedEvent);
            Assert.Equal(expectedTimestamp, receivedEvent.EventTime);
        }

        [Fact]
        public async Task Emit_BasicCallWithDataVersion_Succeeds()
        {
            // Arrange
            const string eventType = "Codito.NewCarRegistered";
            var licensePlate = Guid.NewGuid().ToString();
            var expectedDataVersion = "1337";
            var eventPayload = new { LicensePlate = licensePlate };
            var rawEventPayload = JsonConvert.SerializeObject(eventPayload);

            // Act
            var response = await _eventService.EmitAsync(rawEventPayload, eventType, dataVersion: expectedDataVersion);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var eventId = AssertHttpHeader(Headers.Response.Events.Id, response);
            var receivedEvent = GetReceivedEvent(eventId);
            Assert.NotNull(receivedEvent);
            Assert.Equal(expectedDataVersion, receivedEvent.DataVersion);
        }

        private void AssertReceivedEvent(string eventId, string eventType, string eventTimestamp, string eventSubject, string eventDataVersion, JToken expectedEventPayload)
        {
            var receivedEvent = GetReceivedEvent(eventId);
            var parsedTime = DateTimeOffset.Parse(eventTimestamp);
            Assert.Equal(parsedTime.UtcDateTime, receivedEvent.EventTime.UtcDateTime);
            Assert.Equal(eventType, receivedEvent.EventType);
            Assert.Equal(eventId, receivedEvent.Id);
            Assert.Equal(eventSubject, receivedEvent.Subject);
            Assert.Equal(expectedEventPayload, receivedEvent.Data);
            Assert.Equal(eventDataVersion, receivedEvent.DataVersion);
        }

        private RawEvent GetReceivedEvent(string eventId)
        {
            var receivedEvent = _serviceBusEventConsumerHost.GetReceivedEvent(eventId);
            var rawEvents = EventGridParser.Parse<RawEvent>(receivedEvent);
            Assert.NotNull(rawEvents);
            Assert.NotNull(rawEvents.Events);
            Assert.Single(rawEvents.Events);
            var firstRawEvent = rawEvents.Events.FirstOrDefault();
            Assert.NotNull(firstRawEvent);

            return firstRawEvent;
        }

        private string AssertHttpHeader(string headerName, HttpResponseMessage response)
        {
            var isHeaderFound = response.Headers.TryGetValues(headerName, out var headerValues);
            Assert.True(isHeaderFound);
            var headerValue = headerValues.Single();
            Assert.NotEmpty(headerValue);

            return headerValue;
        }
    }
}