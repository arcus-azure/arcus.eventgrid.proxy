using System;
using Arcus.EventGrid.Sidecar.Api.Validation.Steps.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Arcus.EventGrid.Sidecar.Api.Validation.Steps
{
    public class EventGridAuthKeyValidationStep : IValidationStep
    {
        private readonly IConfiguration _configuration;
        public string StepName { get; } = "Event Grid Authentication Key";

        public EventGridAuthKeyValidationStep(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ValidationResult Execute()
        {
            var authKey = _configuration[EnvironmentVariables.Runtime.EventGrid.AuthKey];

            if (string.IsNullOrWhiteSpace(authKey))
            {
                return ValidationResult.Failure(StepName, "No authentication key was specified");
            }

            return ValidationResult.Success(StepName);
        }
    }
}