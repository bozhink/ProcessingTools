namespace ProcessingTools.Services.Validation.Models
{
    using System;
    using Contracts.Models;
    using ProcessingTools.Enumerations;

    internal class ValidationServiceModel<T> : IValidationServiceModel<T>
    {
        public T ValidatedObject { get; set; }

        public Exception ValidationException { get; set; }

        public ValidationStatus ValidationStatus { get; set; }
    }
}
