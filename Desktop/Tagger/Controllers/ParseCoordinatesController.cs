namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Coordinates;
    using ProcessingTools.Contracts;

    [Description("Parse coordinates.")]
    public class ParseCoordinatesController : TaggerControllerFactory, IParseCoordinatesController
    {
        private readonly ILogger logger;

        public ParseCoordinatesController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var parser = new CoordinatesParser(this.logger);

            await parser.Parse(document.XmlDocument.DocumentElement);
        }
    }
}
