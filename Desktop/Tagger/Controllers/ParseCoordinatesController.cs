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
        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var parser = new CoordinatesParser(document.Xml, logger);

            await parser.Parse();

            document.Xml = parser.Xml;
        }
    }
}
