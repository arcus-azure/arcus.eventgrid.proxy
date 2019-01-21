using Arcus.EventGrid.Sidecar.Api;
using Arcus.EventGrid.Sidecar.Api.Validation.Steps;
using Arcus.EventGrid.Sidecar.Tests.Unit.Stubs;
using Xunit;

namespace Arcus.EventGrid.Sidecar.Tests.Unit.Validation.Steps
{
    [Collection("Unit")]
    public class EventGridAuthKeyValidationStepTests
    {
        [Fact]
        public void Execute_HasValidAuthKey_Succeeds()
        {
            // Arrange
            const string configuredAuthKey = "ABC";
            var configurationStub = new ConfigurationStub(EnvironmentVariables.Runtime.EventGrid.AuthKey, configuredAuthKey);
            var validationStep = new EventGridAuthKeyValidationStep(configurationStub);

            // Act
            var validationResult = validationStep.Execute();

            // Assert
            Assert.True(validationResult.Successful);
        }

        [Fact]
        public void Execute_NoValidAuthKey_ValidationFails()
        {
            // Arrange
            var configurationStub = new ConfigurationStub();
            var validationStep = new EventGridAuthKeyValidationStep(configurationStub);

            // Act
            var validationResult = validationStep.Execute();

            // Assert
            Assert.False(validationResult.Successful);
        }
    }
}