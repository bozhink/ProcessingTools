namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Data.Miners;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories;
    using ProcessingTools.Bio.Taxonomy.Services.Data;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    // TODO: Ninject
    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : TaggerControllerFactory, ITagHigherTaxaController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var miner = new HigherTaxaDataMiner(new TaxonRankDataService(new TaxaRepositoryProvider()));
            var blackListService = new TaxonomicBlackListDataService(new TaxonomicBlackListRepository());

            var tagger = new HigherTaxaTagger(settings.Config, document.OuterXml, miner, blackListService, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml.NormalizeXmlToSystemXml());
        }
    }
}
