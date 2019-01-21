using System;
using Arcus.EventGrid.Sidecar.Api.Validation.Steps.Interfaces;

namespace Arcus.EventGrid.Sidecar.Api.Validation.Steps
{
    public class EventGridAuthKeyValidationStep : IValidationStep
    {
        public string StepName { get; } = "Event Grid Authentication Key";

        public ValidationResult Execute()
        {
            var authKey = Environment.GetEnvironmentVariable(EnvironmentVariables.Runtime.EventGrid.AuthKey);

            if (string.IsNullOrWhiteSpace(authKey))
            {
                return ValidationResult.Failure(StepName, "No authentication key was specified");
            }

            return ValidationResult.Success(StepName);
        }
    }
}