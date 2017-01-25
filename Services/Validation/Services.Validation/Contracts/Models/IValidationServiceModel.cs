namespace ProcessingTools.Services.Validation.Contracts.Models
{
    using System;
    using ProcessingTools.Enumerations;

    public interface IValidationServiceModel<T>
    {
        T ValidatedObject { get; set; }

        ValidationStatus ValidationStatus { get; set; }

        Exception ValidationException { get; set; }
    }
}
