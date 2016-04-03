namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories;
    using ProcessingTools.Bio.Taxonomy.Services.Data;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag lower taxa.")]
    public class TagLowerTaxaController : TaggerControllerFactory, ITagLowerTaxaController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var blackListService = new TaxonomicBlackListDataService(new TaxonomicBlackListRepository());

            var tagger = new LowerTaxaTagger(settings.Config, document.OuterXml, blackListService, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml.NormalizeXmlToSystemXml());
        }
    }
}
