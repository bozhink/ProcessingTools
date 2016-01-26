namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.References;
    using ProcessingTools.Contracts;

    [Description("Parse references.")]
    public class ParseReferencesController : TaggerControllerFactory, IParseReferencesController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new ReferencesParser(settings.Config, document.OuterXml, logger);

            await parser.Parse();

            document.LoadXml(parser.Xml);
        }
    }
}
