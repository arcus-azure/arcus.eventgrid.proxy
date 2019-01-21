using GuardNet;

namespace Arcus.EventGrid.Sidecar.Api.Validation
{
    public class ValidationResult
    {
        private ValidationResult(string stepName, bool isSuccessful) : this(stepName, isSuccessful, errorMessage: string.Empty)
        {
        }

        private ValidationResult(string stepName, bool isSuccessful, string errorMessage)
        {
            Guard.NotNullOrEmpty(stepName, nameof(stepName));

            StepName = stepName;
            Successful = isSuccessful;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        ///     Name of the step
        /// </summary>
        public string StepName { get; }

        /// <summary>
        ///     Indication whether or not validation was successful
        /// </summary>
        public bool Successful { get; }

        /// <summary>
        ///     Error message describing why validation failed
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        ///     Validation was successful
        /// </summary>
        /// <param name="stepName">Name of the step that was validated</param>
        public static ValidationResult Success(string stepName)
        {
            return new ValidationResult(stepName, isSuccessful: true);
        }


        /// <summary>
        ///     Validation failed
        /// </summary>
        /// <param name="stepName">Name of the step that was validated</param>
        public static ValidationResult Failure(string stepName, string errorMessage)
        {
            return new ValidationResult(stepName, isSuccessful: false, errorMessage: errorMessage);
        }
    }
}