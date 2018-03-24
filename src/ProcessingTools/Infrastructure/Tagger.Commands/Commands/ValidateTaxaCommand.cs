namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Validation;

    [System.ComponentModel.Description("Taxa validation using Global Names Resolver.")]
    public class ValidateTaxaCommand : DocumentValidatorCommand<ITaxaValidator>, IValidateTaxaCommand
    {
        public ValidateTaxaCommand(ITaxaValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
