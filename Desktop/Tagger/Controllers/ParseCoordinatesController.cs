namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Coordinates;

    [Description("Parse coordinates.")]
    public class ParseCoordinatesController : IParseCoordinatesController
    {
        private ICoordinatesParser coordinatesParser;

        public ParseCoordinatesController(ICoordinatesParser coordinatesParser)
        {
            if (coordinatesParser == null)
            {
                throw new ArgumentNullException(nameof(coordinatesParser));
            }

            this.coordinatesParser = coordinatesParser;
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

            return await this.coordinatesParser.Parse(document.XmlDocument.DocumentElement);
        }
    }
}
