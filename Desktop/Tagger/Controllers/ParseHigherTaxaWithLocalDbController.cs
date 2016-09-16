namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa with local database.")]
    public class ParseHigherTaxaWithLocalDbController : TaggerControllerFactory, IParseHigherTaxaWithLocalDbController
    {
        private readonly ILocalDbTaxaRankResolverDataService service;

        public ParseHigherTaxaWithLocalDbController(ILocalDbTaxaRankResolverDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new HigherTaxaParserWithDataService<ITaxonRank>(this.service, logger);

            await parser.Parse(document);

            await document.PrintNonParsedTaxa(logger);
        }
    }
}
