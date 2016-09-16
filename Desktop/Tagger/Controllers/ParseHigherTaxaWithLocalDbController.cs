namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

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

        public ParseHigherTaxaWithLocalDbController(IHigherTaxaParserWithDataService<ILocalDbTaxaRankResolverDataService, ITaxonRank> parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            await this.parser.Parse(document);
            await document.PrintNonParsedTaxa(logger);
        }
    }
}
