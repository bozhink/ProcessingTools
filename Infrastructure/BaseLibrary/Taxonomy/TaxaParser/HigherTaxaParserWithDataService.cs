namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Bio.Taxonomy.Contracts;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class HigherTaxaParserWithDataService<T> : ConfigurableDocument, IParser
        where T : ITaxonRank
    {
        private ILogger logger;
        private ITaxaDataService<T> taxaRankDataService;

        public HigherTaxaParserWithDataService(string xml, ITaxaDataService<T> taxaRankDataService, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
            this.taxaRankDataService = taxaRankDataService;
        }

        /// <summary>
        /// This method parses all non-parsed higher taxa by making then of type 'above-genus'
        /// </summary>
        public Task Parse()
        {
            return Task.Run(() =>
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
                                    ////this.logger?.Log("{0} --> {1}\n", scientificName, rank);

                                    string xpath = $"//tn[@type='higher'][not(tn-part)][normalize-space(.)='{scientificName}']";
                                    this.XmlDocument.SelectNodes(xpath)
                                        .Cast<XmlNode>()
                                        .AsParallel()
                                        .ForAll(tn =>
                                        {
                                            XmlElement taxonNamePart = tn.OwnerDocument.CreateElement("tn-part");
                                            taxonNamePart.SetAttribute("type", rank);
                                            taxonNamePart.InnerXml = tn.InnerXml;
                                            tn.InnerXml = taxonNamePart.OuterXml;
                                        });

                                    ////foreach (XmlNode tn in this.XmlDocument.SelectNodes(xpath))
                                    ////{
                                    ////    XmlElement taxonNamePart = tn.OwnerDocument.CreateElement("tn-part");
                                    ////    taxonNamePart.SetAttribute("type", rank);
                                    ////    taxonNamePart.InnerXml = tn.InnerXml;
                                    ////    tn.InnerXml = taxonNamePart.OuterXml;
                                    ////}
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
            });
        }
    }
}