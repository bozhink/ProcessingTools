namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag lower taxa.")]
    public class TagLowerTaxaController : TaggerControllerFactory, ITagLowerTaxaController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var tagger = new LowerTaxaTagger(settings.Config, document.OuterXml, settings.BlackList, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml.NormalizeXmlToSystemXml(settings.Config));
        }
    }
}
