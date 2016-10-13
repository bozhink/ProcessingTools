namespace ProcessingTools.Tagger.Factories
{
    using System.Threading.Tasks;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseHigherTaxaControllerFactory<TService> : TaggerControllerFactory
        where TService : ITaxonRankResolverDataService
    {
        private readonly ILogger logger;

        public ParseHigherTaxaControllerFactory(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected abstract IHigherTaxaParserWithDataService<TService, ITaxonRank> Parser { get; }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.Parser.Parse(document.XmlDocument.DocumentElement);
            await document.XmlDocument.PrintNonParsedTaxa(this.logger);
        }
    }
}
