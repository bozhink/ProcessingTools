namespace ProcessingTools.Services.Validation.Models
{
    using System;
    using Contracts;
    using ProcessingTools.Contracts.Types;

    public class ValidationServiceModel<T> : IValidationServiceModel<T>
    {
        public T ValidatedObject { get; set; }

        public Exception ValidationException { get; set; }

        public ValidationStatus ValidationStatus { get; set; }
    }
}