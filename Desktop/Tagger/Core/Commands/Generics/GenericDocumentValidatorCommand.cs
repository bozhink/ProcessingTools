namespace ProcessingTools.Tagger.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;

    public class GenericDocumentValidatorCommand<TValidator> : ITaggerCommand
        where TValidator : IDocumentValidator
    {
        private readonly TValidator validator;

        public GenericDocumentValidatorCommand(TValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            this.validator = validator;
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

            return await this.validator.Validate(document);
        }
    }
}
