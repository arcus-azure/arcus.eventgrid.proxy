using System;
using System.Collections.Generic;
using Arcus.EventGrid.Sidecar.Api.Validation.Steps;
using Arcus.EventGrid.Sidecar.Api.Validation.Steps.Interfaces;

namespace Arcus.EventGrid.Sidecar.Api.Validation
{
    public class RuntimeValidator
    {
        private static readonly List<IValidationStep> validationSteps = new List<IValidationStep>
        {
            new EventGridAuthKeyValidationStep(),
            new EventGridTopicEndpointValidationStep()
        };

        public static List<ValidationResult> Run()
        {
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