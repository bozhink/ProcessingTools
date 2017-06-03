namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Validation;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Taxa validation using Global Names Resolver.")]
    public class ValidateTaxaCommand : GenericDocumentValidatorCommand<ITaxaValidator>, IValidateTaxaCommand
    {
        public ValidateTaxaCommand(ITaxaValidator validator, IReporter reporter)
            : base(validator, reporter)
        {
        }
    }
}
