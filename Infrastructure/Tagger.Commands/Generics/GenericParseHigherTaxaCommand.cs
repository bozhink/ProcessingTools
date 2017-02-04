namespace ProcessingTools.Tagger.Commands.Generics
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    public class GenericParseHigherTaxaCommand<TService> : ITaggerCommand
        where TService : ITaxaRankResolver
    {
        private readonly IHigherTaxaParserWithDataService<TService, ITaxonRank> parser;
        private readonly IReporter reporter;

        public GenericParseHigherTaxaCommand(
            IHigherTaxaParserWithDataService<TService, ITaxonRank> parser,
            IReporter reporter)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            this.parser = parser;
            this.reporter = reporter;
        }

        public async Task<object> Run(IDocument document, ICommandSettings settings)
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

            await this.PrintNonParsedTaxa(document.XmlDocument);

            return result;
        }

        public async Task PrintNonParsedTaxa(XmlDocument xmlDocument)
        {
            var uniqueHigherTaxaList = xmlDocument.ExtractUniqueNonParsedHigherTaxa()
                .Distinct()
                .OrderBy(s => s)
                .ToArray();

            if (uniqueHigherTaxaList.Length > 0)
            {
                this.reporter.AppendContent("Non-parsed taxa:");
                foreach (var taxonName in uniqueHigherTaxaList)
                {
                    this.reporter.AppendContent(string.Format("\t{0}", taxonName));
                }

                await this.reporter.MakeReport();
            }
        }
    }
}
