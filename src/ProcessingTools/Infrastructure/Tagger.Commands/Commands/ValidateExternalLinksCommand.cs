namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands.Tagger;
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
