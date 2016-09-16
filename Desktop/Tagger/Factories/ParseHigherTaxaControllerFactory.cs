namespace ProcessingTools.Tagger.Factories
{
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseHigherTaxaControllerFactory<TService> : TaggerControllerFactory
        where TService : ITaxonRankResolverDataService
    {
        protected abstract IHigherTaxaParserWithDataService<TService, ITaxonRank> Parser { get; }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            await this.Parser.Parse(document.DocumentElement);
            await document.PrintNonParsedTaxa(logger);
        }
    }
}
