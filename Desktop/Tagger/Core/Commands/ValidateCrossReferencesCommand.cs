namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Validation;

    [Description("Validate cross-references.")]
    public class ValidateCrossReferencesCommand : GenericDocumentValidatorCommand<ICrossReferencesValidator>, IValidateCrossReferencesCommand
    {
        public ValidateCrossReferencesCommand(ICrossReferencesValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
