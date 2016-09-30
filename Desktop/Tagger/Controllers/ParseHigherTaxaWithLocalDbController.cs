namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa with local database.")]
    public class ParseHigherTaxaWithLocalDbController : TaggerControllerFactory, IParseHigherTaxaWithLocalDbController
    {
        private readonly IHigherTaxaParserWithDataService<ILocalDbTaxaRankResolverDataService, ITaxonRank> parser;

        public ParseHigherTaxaWithLocalDbController(
            IDocumentFactory documentFactory,
            IHigherTaxaParserWithDataService<ILocalDbTaxaRankResolverDataService, ITaxonRank> parser)
            : base(documentFactory)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            await this.parser.Parse(document.XmlDocument);
            await document.XmlDocument.PrintNonParsedTaxa(logger);
        }
    }
}
