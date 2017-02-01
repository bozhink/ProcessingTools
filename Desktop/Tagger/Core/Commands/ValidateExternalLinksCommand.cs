namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Validation;

    [Description("Validate external links.")]
    public class ValidateExternalLinksCommand : GenericDocumentValidatorCommand<IExternalLinksValidator>, IValidateExternalLinksCommand
    {
        public ValidateExternalLinksCommand(IExternalLinksValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
