namespace ProcessingTools.Services.Common.Models.Contracts
{
    using System;
    using Types;

    public interface IValidationServiceModel<T>
    {
        T ValidatedObject { get; set; }

        ValidationStatus ValidationStatus { get; set; }

        Exception ValidationException { get; set; }
    }
}