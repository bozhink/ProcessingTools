namespace ProcessingTools.Services.Validation.Models
{
    using System;

    using Contracts;
    using ProcessingTools.Services.Common.Types;

    public class UrlValidationServiceModel : IUrlValidationServiceModel
    {
        public IUrl ValidatedObject { get; set; }

        public Exception ValidationException { get; set; }

        public ValidationStatus ValidationStatus { get; set; }
    }
}
