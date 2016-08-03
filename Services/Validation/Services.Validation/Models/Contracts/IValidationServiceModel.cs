namespace ProcessingTools.Services.Validation.Models.Contracts
{
    using System;
    using ProcessingTools.Contracts.Types;

    public interface IValidationServiceModel<T>
    {
        T ValidatedObject { get; set; }

        ValidationStatus ValidationStatus { get; set; }

        Exception ValidationException { get; set; }
    }
}