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

        public override string ToString()
        {
            return string.Format("{0} / {1}", this.ValidatedObject, this.ValidationStatus);
        }
    }
}
