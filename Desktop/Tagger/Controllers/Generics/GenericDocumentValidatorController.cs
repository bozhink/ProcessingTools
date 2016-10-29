namespace ProcessingTools.Tagger.Controllers.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Controllers;
    using ProcessingTools.Contracts;

    public class GenericDocumentValidatorController<TValidator> : ITaggerController
        where TValidator : IDocumentValidator
    {
        private readonly TValidator validator;

        public GenericDocumentValidatorController(TValidator validator)
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
