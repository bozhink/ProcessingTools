namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    [Description("Parse coordinates.")]
    public class ParseCoordinatesController : TaggerControllerFactory, IParseCoordinatesController
    {
        private ICoordinatesParser coordinatesParser;

        public ParseCoordinatesController(IDocumentFactory documentFactory, ICoordinatesParser coordinatesParser)
            : base(documentFactory)
        {
            if (coordinatesParser == null)
            {
                throw new ArgumentNullException(nameof(coordinatesParser));
            }

            this.coordinatesParser = coordinatesParser;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.coordinatesParser.Parse(document.XmlDocument.DocumentElement);
        }
    }
}
