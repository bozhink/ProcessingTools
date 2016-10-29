namespace ProcessingTools.Tagger.Controllers.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Contracts;

    public class GenericDocumentFormatterController<TFormatter> : ITaggerController
        where TFormatter : IDocumentFormatter
    {
        private readonly TFormatter formatter;

        public GenericDocumentFormatterController(TFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.formatter = formatter;
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

            return await this.formatter.Format(document);
        }
    }
}