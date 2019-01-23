using System;
using System.Collections.Generic;
using Arcus.EventGrid.Proxy.Api.Validation.Steps;
using Arcus.EventGrid.Proxy.Api.Validation.Steps.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Arcus.EventGrid.Proxy.Api.Validation
{
    public class RuntimeValidator
    {
        public static List<ValidationResult> Run(IConfiguration configuration)
        {
            var validationSteps = new List<IValidationStep>
            {
                new EventGridAuthKeyValidationStep(configuration),
                new EventGridTopicEndpointValidationStep(configuration)
            };

            Console.WriteLine("Starting validation of runtime configuration...");
            List<ValidationResult> validationOutcomes = new List<ValidationResult>();
            foreach (var validationStep in validationSteps)
            {
                var validationOutcome = validationStep.Execute();
                validationOutcomes.Add(validationOutcome);
            }

            validationOutcomes.ForEach(LogValidationResults);
            Console.WriteLine("Validation of runtime configuration completed.");

            return validationOutcomes;
        }

        private static void LogValidationResults(ValidationResult validationResult)
        {
            var validationOutcomeMessage = validationResult.Successful ? "successful" : "failed";
            var validationInfoMessage = $"Validation step '{validationResult.StepName}' {validationOutcomeMessage}.";
            if (validationResult.Successful == false)
            {
                validationInfoMessage += $" Error: {validationResult.ErrorMessage}";
            }

            Console.WriteLine(validationInfoMessage);
        }
    }
}