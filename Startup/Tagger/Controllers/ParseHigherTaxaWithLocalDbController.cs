namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data;
    using ProcessingTools.Contracts;

    public class ParseHigherTaxaWithLocalDbController : TaggerControllerFactory, IParseHigherTaxaWithLocalDbController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var service = new LocalDbTaxaRankDataService(settings.Config.RankListXmlFilePath);
            var parser = new HigherTaxaParserWithDataService<ITaxonRank>(document.OuterXml, service, logger);

            await parser.Parse();

            await parser.XmlDocument.PrintNonParsedTaxa(logger);

            document.LoadXml(parser.Xml);
        }
    }
}
