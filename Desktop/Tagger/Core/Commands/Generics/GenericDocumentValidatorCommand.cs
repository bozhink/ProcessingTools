namespace ProcessingTools.Tagger.Core.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;

    public class GenericDocumentValidatorCommand<TValidator> : ITaggerCommand
        where TValidator : IDocumentValidator
    {
        private readonly IReporter reporter;
        private readonly TValidator validator;

        public GenericDocumentValidatorCommand(TValidator validator, IReporter reporter)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            this.validator = validator;
            this.reporter = reporter;
        }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var result = await this.validator.Validate(document, this.reporter);

            await this.reporter.MakeReport();

            return result;
        }
    }
}
