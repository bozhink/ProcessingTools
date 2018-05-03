namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Validation;

    [System.ComponentModel.Description("Validate cross-references.")]
    public class ValidateCrossReferencesCommand : DocumentValidatorCommand<ICrossReferencesValidator>, IValidateCrossReferencesCommand
    {
        public ValidateCrossReferencesCommand(ICrossReferencesValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
