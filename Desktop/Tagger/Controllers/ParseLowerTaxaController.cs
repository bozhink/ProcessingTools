namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;

    [Description("Parse lower taxa.")]
    public class ParseLowerTaxaController : TaggerControllerFactory, IParseLowerTaxaController
    {
        protected override async Task Run(IDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new LowerTaxaParser(document.Xml, logger);

            await parser.Parse();

            document.Xml = parser.Xml;
        }
    }
}
