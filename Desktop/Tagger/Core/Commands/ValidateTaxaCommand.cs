namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Validation;

    [Description("Taxa validation using Global Names Resolver.")]
    public class ValidateTaxaCommand : GenericDocumentValidatorCommand<ITaxaValidator>, IValidateTaxaCommand
    {
        public ValidateTaxaCommand(ITaxaValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
