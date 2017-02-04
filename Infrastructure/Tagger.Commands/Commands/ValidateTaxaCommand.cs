namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Validation;

    [Description("Taxa validation using Global Names Resolver.")]
    public class ValidateTaxaCommand : GenericDocumentValidatorCommand<ITaxaValidator>, IValidateTaxaCommand
    {
        public ValidateTaxaCommand(ITaxaValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
