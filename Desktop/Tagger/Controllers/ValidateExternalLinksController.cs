namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Validation;

    [Description("Validate external links.")]
    public class ValidateExternalLinksController : GenericDocumentValidatorController<IExternalLinksValidator>, IValidateExternalLinksController
    {
        public ValidateExternalLinksController(IExternalLinksValidator validator)
            : base(validator)
        {
        }
    }
}
