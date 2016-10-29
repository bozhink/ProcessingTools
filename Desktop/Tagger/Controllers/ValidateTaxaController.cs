namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Validation;

    [Description("Taxa validation using Global Names Resolver.")]
    public class ValidateTaxaController : GenericDocumentValidatorController<ITaxaValidator>, IValidateTaxaController
    {
        public ValidateTaxaController(ITaxaValidator validator)
            : base(validator)
        {
        }
    }
}
