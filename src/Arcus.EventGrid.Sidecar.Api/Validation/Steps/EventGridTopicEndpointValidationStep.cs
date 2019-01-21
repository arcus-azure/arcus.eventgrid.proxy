using System;
using Arcus.EventGrid.Sidecar.Api.Validation.Steps.Interfaces;

namespace Arcus.EventGrid.Sidecar.Api.Validation.Steps
{
    public class EventGridTopicEndpointValidationStep : IValidationStep
    {
        public string StepName { get; } = "Event Grid Topic Endpoint";

        public ValidationResult Execute()
        {
            var rawTopicEndpoint = Environment.GetEnvironmentVariable(EnvironmentVariables.Runtime.EventGrid.TopicEndpoint);

            if (string.IsNullOrWhiteSpace(rawTopicEndpoint))
            {
                return ValidationResult.Failure(StepName, "No topic endpoint was specified");
            }

            try
            {
                var uri = new Uri(rawTopicEndpoint);

                return ValidationResult.Success(StepName);
            }
            catch
            {
                return ValidationResult.Failure(StepName, "No valid topic endpoint was specified");
            }
        }
    }
}