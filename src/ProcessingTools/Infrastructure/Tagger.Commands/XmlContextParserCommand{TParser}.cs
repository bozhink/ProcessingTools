namespace ProcessingTools.Tagger.Commands
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors;

    public class XmlContextParserCommand<TParser> : ITaggerCommand
        where TParser : class, IXmlContextParser
    {
        private readonly TParser parser;

        public XmlContextParserCommand(TParser parser)
        {
            this.parser = parser ?? throw new ArgumentNullException(nameof(parser));
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

            return await this.parser.ParseAsync(document.XmlDocument.DocumentElement).ConfigureAwait(false);
        }
    }
}
