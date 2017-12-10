namespace ProcessingTools.Tagger.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors;

    public class GenericDocumentValidatorCommand<TValidator> : ITaggerCommand
        where TValidator : class, IDocumentValidator
    {
        private readonly IReporter reporter;
        private readonly TValidator validator;

        public GenericDocumentValidatorCommand(TValidator validator, IReporter reporter)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var result = await this.validator.ValidateAsync(document, this.reporter).ConfigureAwait(false);
            await this.reporter.MakeReportAsync().ConfigureAwait(false);

            return result;
        }
    }
}
