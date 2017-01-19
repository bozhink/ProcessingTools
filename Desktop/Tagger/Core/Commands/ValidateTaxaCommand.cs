namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
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
