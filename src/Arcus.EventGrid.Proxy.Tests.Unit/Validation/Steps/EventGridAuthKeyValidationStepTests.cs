using Arcus.EventGrid.Proxy.Api;
using Arcus.EventGrid.Proxy.Api.Validation.Steps;
using Arcus.EventGrid.Proxy.Tests.Unit.Stubs;
using Xunit;

namespace Arcus.EventGrid.Proxy.Tests.Unit.Validation.Steps
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
            var validationStep = new EventGridAuthKValidationStep(configurationStub);

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
