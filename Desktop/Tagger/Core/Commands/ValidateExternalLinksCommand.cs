namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Validation;

    [Description("Validate external links.")]
    public class ValidateExternalLinksCommand : GenericDocumentValidatorCommand<IExternalLinksValidator>, IValidateExternalLinksCommand
    {
        public ValidateExternalLinksCommand(IExternalLinksValidator validator)
            : base(validator)
        {
        }
    }
}
