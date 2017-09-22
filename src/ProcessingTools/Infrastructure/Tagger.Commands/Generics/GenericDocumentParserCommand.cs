namespace ProcessingTools.Tagger.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    public class GenericDocumentParserCommand<TParser> : ITaggerCommand
        where TParser : class, IDocumentParser
    {
        private readonly TParser parser;

        public GenericDocumentParserCommand(TParser parser)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public async Task<object> Run(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return await this.parser.ParseAsync(document).ConfigureAwait(false);
        }
    }
}
