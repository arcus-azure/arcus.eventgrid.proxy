namespace Arcus.EventGrid.Sidecar.Api.Validation.Steps.Interfaces
{
    public interface IValidationStep
    {
        /// <summary>
        ///     Name of the validation step
        /// </summary>
        string StepName { get; }

        /// <summary>
        ///     Executes the validation step
        /// </summary>
        ValidationResult Execute();
    }
}