﻿namespace ProcessingTools.Tagger.Factories
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseHigherTaxaControllerFactory<TService> : ITaggerController
        where TService : ITaxonRankResolverDataService
    {
        private readonly IHigherTaxaParserWithDataService<TService, ITaxonRank> parser;
        private readonly ILogger logger;

        public ParseHigherTaxaControllerFactory(
            IHigherTaxaParserWithDataService<TService, ITaxonRank> parser,
            ILogger logger)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
            this.logger = logger;
        }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var result = await this.parser.Parse(document.XmlDocument.DocumentElement);
            await document.XmlDocument.PrintNonParsedTaxa(this.logger);

            return result;
        }
    }
}
