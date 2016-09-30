namespace ProcessingTools.Tagger.Factories
{
    using System.Threading.Tasks;

    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseHigherTaxaControllerFactory<TService> : TaggerControllerFactory
        where TService : ITaxonRankResolverDataService
    {
        protected abstract IHigherTaxaParserWithDataService<TService, ITaxonRank> Parser { get; }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            await this.Parser.Parse(document.XmlDocument.DocumentElement);
            await document.XmlDocument.PrintNonParsedTaxa(logger);
        }
    }
}
