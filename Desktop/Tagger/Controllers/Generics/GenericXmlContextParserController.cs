namespace ProcessingTools.Tagger.Controllers.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Contracts;

    public class GenericXmlContextParserController<TParser> : ITaggerController
        where TParser : IXmlContextParser
    {
        private readonly TParser parser;

        public GenericXmlContextParserController(TParser parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
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

            return await this.parser.Parse(document.XmlDocument.DocumentElement);
        }
    }
}
