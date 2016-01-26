namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Coordinates;
    using ProcessingTools.Contracts;

    [Description("Parse coordinates.")]
    public class ParseCoordinatesController : TaggerControllerFactory, IParseCoordinatesController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new CoordinatesParser(settings.Config, document.OuterXml, logger);

            await parser.Parse();

            document.LoadXml(parser.Xml);
        }
    }
}
