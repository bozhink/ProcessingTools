namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Data.Miners;
    using ProcessingTools.Bio.Taxonomy.Data.Xml;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories;
    using ProcessingTools.Bio.Taxonomy.Services.Data;
    using ProcessingTools.Contracts;

    // TODO: Ninject
    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : TaggerControllerFactory, ITagHigherTaxaController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var contextProvider = new TaxaContextProvider();
            var repositoryProvider = new XmlTaxonRankRepositoryProvider(contextProvider);
            var miner = new HigherTaxaDataMiner(new TaxonRankDataService(repositoryProvider));
            var blackListService = new TaxonomicBlackListDataService(new TaxonomicBlackListRepository());

            var tagger = new HigherTaxaTagger(document.OuterXml, miner, blackListService, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml.NormalizeXmlToSystemXml());
        }
    }
}
