namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Validation;

    [System.ComponentModel.Description("Validate cross-references.")]
    public class ValidateCrossReferencesCommand : DocumentValidatorCommand<ICrossReferencesValidator>, IValidateCrossReferencesCommand
    {
        public ValidateCrossReferencesCommand(ICrossReferencesValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
