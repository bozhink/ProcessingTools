namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Models;

    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Extensions;

    public class HigherTaxaParserWithDataService<T> : TaxPubDocument, IParser
        where T : ITaxonRank
    {
        private ILogger logger;
        private ITaxonRankResolverDataService taxaRankDataService;

        public HigherTaxaParserWithDataService(string xml, ITaxonRankResolverDataService taxaRankDataService, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
            this.taxaRankDataService = taxaRankDataService;
        }

        public async Task Parse()
        {
            var uniqueHigherTaxaList = new HashSet<string>(this.XmlDocument
                .ExtractUniqueHigherTaxa()
                .Select(n => n.ToFirstLetterUpperCase()))
                .ToArray();

            if (uniqueHigherTaxaList.Length < 1)
            {
                return;
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

            if (resolvedTaxa.Count() < 1)
            {
                return;
            }

            foreach (var scientificName in uniqueHigherTaxaList)
            {
                var ranks = resolvedTaxa
                    .Where(t => t.ScientificName == scientificName)
                    .Select(t => t.Rank)
                    .Where(r => !string.IsNullOrWhiteSpace(r))
                    .ToList();

                int numberOfRanks = ranks?.Count ?? 0;
                switch (numberOfRanks)
                {
                    case 0:
                        this.ProcessZeroRanksCase(scientificName, ranks);
                        break;

                    case 1:
                        this.ProcessSingleRankCase(scientificName, ranks);
                        break;

                    default:
                        this.ProcessMultipleRanksCase(scientificName, ranks);
                        break;
                }
            }
        }

        private void ProcessMultipleRanksCase(string scientificName, IEnumerable<string> ranks)
        {
            this.logger?.Log(LogType.Warning, "{0} --> Multiple matches.", scientificName);
            foreach (var rank in ranks)
            {
                this.logger?.Log("{0} --> {1}", scientificName, rank);
            }

            this.logger?.Log();
        }

        private void ProcessSingleRankCase(string scientificName, IEnumerable<string> ranks)
        {
            string rank = ranks.Single();

            string xpath = $"//tn[@type='higher'][not(tn-part)][translate(normalize-space(.),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='{scientificName.ToUpper()}']";

            this.XmlDocument.SelectNodes(xpath)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(tn =>
                {
                    XmlElement taxonNamePart = tn.OwnerDocument.CreateElement(XmlInternalSchemaConstants.TaxonNamePartElementName);
                    taxonNamePart.SetAttribute(XmlInternalSchemaConstants.TaxonNameTypeAttributeName, rank);
                    taxonNamePart.InnerXml = tn.InnerXml;
                    tn.InnerXml = taxonNamePart.OuterXml;
                });
        }

        private void ProcessZeroRanksCase(string scientificName, IEnumerable<string> ranks)
        {
            this.logger?.Log(LogType.Warning, "{0} --> No match or error.\n", scientificName);
        }
    }
}
