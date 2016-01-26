namespace ProcessingTools.MainProgram.Controllers
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
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new LowerTaxaParser(settings.Config, document.OuterXml, logger);

            await parser.Parse();

            document.LoadXml(parser.Xml);
        }
    }
}
