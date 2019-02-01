using System;
using Arcus.EventGrid.Proxy.Api.Validation.Steps.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Arcus.EventGrid.Proxy.Api.Validation.Steps
{
    public class EventGridTopicEndpointValidationStep : IValidationStep
    {
        private readonly IConfiguration _configuration;

        public EventGridTopicEndpointValidationStep(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string StepName { get; } = "Event Grid Topic Endpoint";

        public ValidationResult Execute()
        {
            var rawTopicEndpoint = _configuration[EnvironmentVariables.Runtime.EventGrid.TopicEndpoint];

            if (string.IsNullOrWhiteSpace(rawTopicEndpoint))
            {
                return ValidationResult.Failure(StepName, "No topic endpoint was specified");
            }

            if (Uri.IsWellFormedUriString(rawTopicEndpoint, UriKind.Absolute) == false)
            {
                return ValidationResult.Failure(StepName, "Topic endpoint is not a well-formed URI");
            }

            return ValidationResult.Success(StepName);
        }
    }
}