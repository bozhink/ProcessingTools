namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Strings.Extensions;

    public class HigherTaxaParserWithDataService<TService, T> : IHigherTaxaParserWithDataService<TService, T>
        where TService : ITaxaRankResolver
        where T : ITaxonRank
    {
        private readonly TService taxaRankDataService;
        private readonly ILogger logger;

        public HigherTaxaParserWithDataService(TService taxaRankDataService, ILogger logger)
        {
            if (taxaRankDataService == null)
            {
                throw new ArgumentNullException(nameof(taxaRankDataService));
            }

            this.taxaRankDataService = taxaRankDataService;
            this.logger = logger;
        }

        public async Task<long> Parse(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var uniqueHigherTaxaList = new HashSet<string>(context
                .ExtractUniqueNonParsedHigherTaxa()
                .Select(n => n.ToFirstLetterUpperCase()))
                .ToArray();

            long numberOfUniqueHigherTaxa = uniqueHigherTaxaList.LongLength;
            if (numberOfUniqueHigherTaxa < 1L)
            {
                return numberOfUniqueHigherTaxa;
            }

            var response = await this.taxaRankDataService.Resolve(uniqueHigherTaxaList);
            if (response == null)
            {
                throw new ApplicationException("Current taxa rank data service instance returned null.");
            }

            var resolvedTaxa = response
                .Select(t => new TaxonRankResponseModel
                {
                    ScientificName = t.ScientificName,
                    Rank = t.Rank.MapTaxonRankTypeToTaxonRankString()
                });

            long numberOfResolvedTaxa = resolvedTaxa.LongCount();
            if (numberOfResolvedTaxa > 0L)
            {
                foreach (var scientificName in uniqueHigherTaxaList)
                {
                    var ranks = resolvedTaxa
                        .Where(t => t.ScientificName.ToLower() == scientificName.ToLower())
                        .Where(t => !string.IsNullOrWhiteSpace(t.Rank))
                        .Select(t => t.Rank.ToLower())
                        .Distinct()
                        .ToList();

                    int numberOfRanks = ranks?.Count ?? 0;
                    switch (numberOfRanks)
                    {
                        case 0:
                            this.ProcessZeroRanksCase(scientificName, ranks, context);
                            break;

                        case 1:
                            this.ProcessSingleRankCase(scientificName, ranks, context);
                            break;

                        default:
                            this.ProcessMultipleRanksCase(scientificName, ranks, context);
                            break;
                    }
                }
            }

            return numberOfResolvedTaxa;
        }

        private void ProcessMultipleRanksCase(string scientificName, IEnumerable<string> ranks, XmlNode context)
        {
            this.logger?.Log(LogType.Warning, "{0} --> Multiple matches.", scientificName);
            foreach (var rank in ranks)
            {
                this.logger?.Log("{0} --> {1}", scientificName, rank);
            }

            this.logger?.Log();
        }

        private void ProcessSingleRankCase(string scientificName, IEnumerable<string> ranks, XmlNode context)
        {
            string rank = ranks.Single();

            string xpath = $".//tn[@type='higher'][not(tn-part)][translate(normalize-space(.),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='{scientificName.ToUpper()}']";

            context?.SelectNodes(xpath)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(tn =>
                {
                    XmlElement taxonNamePart = tn.OwnerDocument.CreateElement(ElementNames.TaxonNamePart);
                    taxonNamePart.SetAttribute(AttributeNames.Type, rank);
                    taxonNamePart.InnerXml = tn.InnerXml;
                    tn.InnerXml = taxonNamePart.OuterXml;
                });
        }

        private void ProcessZeroRanksCase(string scientificName, IEnumerable<string> ranks, XmlNode context)
        {
            this.logger?.Log(LogType.Warning, "{0} --> No match or error.\n", scientificName);
        }
    }
}
