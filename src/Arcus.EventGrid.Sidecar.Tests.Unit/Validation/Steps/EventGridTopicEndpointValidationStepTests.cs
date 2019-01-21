using Arcus.EventGrid.Sidecar.Api;
using Arcus.EventGrid.Sidecar.Api.Validation.Steps;
using Arcus.EventGrid.Sidecar.Tests.Unit.Stubs;
using Xunit;

namespace Arcus.EventGrid.Sidecar.Tests.Unit.Validation.Steps
{
    [Collection("Unit")]
    public class EventGridTopicEndpointValidationStepTests
    {
        [Fact]
        public void Execute_HasValidTopicEndpoint_Succeeds()
        {
            // Arrange
            const string configuredTopicEndpoint = "https://arcus.com";
            var configurationStub = new ConfigurationStub(EnvironmentVariables.Runtime.EventGrid.TopicEndpoint, configuredTopicEndpoint);
            var validationStep = new EventGridTopicEndpointValidationStep(configurationStub);

            // Act
            var validationResult = validationStep.Execute();

            // Assert
            Assert.True(validationResult.Successful);
        }

        [Fact]
        public void Execute_NoValidTopicEndpoint_ValidationFails()
        {
            // Arrange
            var configurationStub = new ConfigurationStub();
            var validationStep = new EventGridTopicEndpointValidationStep(configurationStub);

            // Act
            var validationResult = validationStep.Execute();

            // Assert
            Assert.False(validationResult.Successful);
        }

        [Fact]
        public void Execute_InvalidTopicEndpoint_ValidationFails()
        {
            // Arrange
            const string configuredTopicEndpoint = "arcus";
            var configurationStub = new ConfigurationStub(EnvironmentVariables.Runtime.EventGrid.TopicEndpoint, configuredTopicEndpoint);
            var validationStep = new EventGridTopicEndpointValidationStep(configurationStub);

            // Act
            var validationResult = validationStep.Execute();

            // Assert
            Assert.False(validationResult.Successful);
        }
    }
}