namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Validation;

    [System.ComponentModel.Description("Validate external links.")]
    public class ValidateExternalLinksCommand : DocumentValidatorCommand<IExternalLinksValidator>, IValidateExternalLinksCommand
    {
        public ValidateExternalLinksCommand(IExternalLinksValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
