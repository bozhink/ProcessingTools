namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Validation;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Validate cross-references.")]
    public class ValidateCrossReferencesCommand : GenericDocumentValidatorCommand<ICrossReferencesValidator>, IValidateCrossReferencesCommand
    {
        public ValidateCrossReferencesCommand(ICrossReferencesValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
