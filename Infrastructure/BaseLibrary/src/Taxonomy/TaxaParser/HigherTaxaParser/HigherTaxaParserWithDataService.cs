namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Bio.Taxonomy.Services.Data.Contracts;
    using Contracts.Log;
    using Models;

    public class HigherTaxaParserWithDataService : Base, IBaseParser
    {
        private ILogger logger;
        private ITaxaRankDataService taxaRankDataService;

        public HigherTaxaParserWithDataService(string xml, ITaxaRankDataService taxaRankDataService, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
            this.taxaRankDataService = taxaRankDataService;
        }

        public HigherTaxaParserWithDataService(IBase baseObject, ITaxaRankDataService taxaRankDataService, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
            this.taxaRankDataService = taxaRankDataService;
        }

        /// <summary>
        /// This method parses all non-parsed higher taxa by making then of type 'above-genus'
        /// </summary>
        public void Parse()
        {
            var uniqueHigherTaxaList = this.XmlDocument
                .ExtractUniqueHigherTaxa()
                .ToArray();

            var response = this.taxaRankDataService.Resolve(uniqueHigherTaxaList);
            if (response == null)
            {
                throw new ApplicationException("Current taxa rank data service instance returned null.");
            }

            var resolvedTaxa = response
                .Select(t => new TaxonRankResponseModel
                {
                    ScientificName = t.ScientificName,
                    Rank = t.Rank
                });

            if (resolvedTaxa.Count() > 0)
            {
                foreach (var scientificName in uniqueHigherTaxaList)
                {
                    var ranks = resolvedTaxa
                        .Where(t => t.ScientificName == scientificName)
                        .Select(t => t.Rank)
                        .ToList();

                    if (ranks == null)
                    {
                        this.logger.Log(LogType.Error, "{0} -> Null ranks object.", scientificName);
                        continue;
                    }

                    int numberOfRanks = ranks.Count;

                    switch (numberOfRanks)
                    {
                        case 0:
                            {
                                this.logger?.Log(LogType.Warning, "{0} --> No match or error.\n", scientificName);
                            }

                            break;

                        case 1:
                            {
                                string rank = ranks.FirstOrDefault();
                                this.logger?.Log("{0} --> {1}\n", scientificName, rank);
                                string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();
                                this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                            }

                            break;

                        default:
                            {
                                this.logger?.Log(LogType.Warning, "{0} --> Multiple matches.", scientificName);
                                foreach (var rank in ranks)
                                {
                                    this.logger?.Log("{0} --> {1}", scientificName, rank);
                                }

                                this.logger?.Log();
                            }

                            break;
                    }
                }
            }
        }

        private void ReplaceTaxonNameByItsParsedContent(string scientificName, string replacement)
        {
            this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tn>)", replacement);
        }
    }
}