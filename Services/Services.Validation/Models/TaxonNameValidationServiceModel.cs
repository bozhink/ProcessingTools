namespace ProcessingTools.Services.Validation.Models
{
    using System;

    using Common.Types;
    using Contracts;

    public class TaxonNameValidationServiceModel : ITaxonNameValidationServiceModel
    {
        public ITaxonName ValidatedObject { get; set; }

        public Exception ValidationException { get; set; }

        public ValidationStatus ValidationStatus { get; set; }
    }
}