namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Validation;

    [Description("Validate cross-references.")]
    public class ValidateCrossReferencesController : GenericDocumentValidatorController<ICrossReferencesValidator>, IValidateCrossReferencesController
    {
        public ValidateCrossReferencesController(ICrossReferencesValidator validator)
            : base(validator)
        {
        }
    }
}
