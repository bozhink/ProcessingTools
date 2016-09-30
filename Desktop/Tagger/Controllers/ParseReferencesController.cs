namespace ProcessingTools.Tagger.Controllers
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
        protected override async Task Run(IDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new ReferencesParser(document.Xml, logger);

            await parser.Parse();

            document.Xml = parser.Xml;
        }
    }
}
