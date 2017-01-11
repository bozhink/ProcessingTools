namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public class TreatmentMetaParserWithDataService<TService> : ITreatmentMetaParserWithDataService<TService>
        where TService : ITaxaClassificationResolver
    {
        private const string SelectTreatmentGeneraXPathString = ".//tp:taxon-treatment[string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'])='ORDO' or string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'])='FAMILIA']/tp:nomenclature/tn/tn-part[@type='genus']";

        private const string TreatmentMetaReplaceXPathTemplate = ".//tp:taxon-treatment[string(tp:nomenclature/tn/tn-part[@type='genus'])='{0}' or string(tp:nomenclature/tn/tn-part[@type='genus']/@full-name)='{0}']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='{1}']";

        private readonly TService service;
        private readonly ILogger logger;

        public TreatmentMetaParserWithDataService(TService service, ILogger logger)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
            this.logger = logger;
        }

        public async Task<object> Parse(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var genusList = document.SelectNodes(SelectTreatmentGeneraXPathString)
                .Select(node => new TaxonNamePart(node))
                .Select(t => t.FullName)
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .ToArray();

            var response = await this.service.Resolve(genusList);

            foreach (string genus in genusList)
            {
                this.logger?.Log("\n{0}\n", genus);

                try
                {
                    var classification = response.Where(r => r.Classification.Single(c => c.Rank == TaxonRankType.Genus).ScientificName == genus);

                    this.ReplaceTreatmentMetaClassificationItem(document, classification, genus, TaxonRankType.Kingdom);
                    this.ReplaceTreatmentMetaClassificationItem(document, classification, genus, TaxonRankType.Order);
                    this.ReplaceTreatmentMetaClassificationItem(document, classification, genus, TaxonRankType.Family);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e);
                }
            }

            return true;
        }

        private void ReplaceTreatmentMetaClassificationItem(IDocument document, IEnumerable<ITaxonClassification> classification, string genus, TaxonRankType rank)
        {
            if (classification == null)
            {
                throw new ArgumentNullException(nameof(classification));
            }

            var matchingHigherTaxa = classification
                .Select(c => c.Classification.SingleOrDefault(x => x.Rank == rank).ScientificName)
                .Distinct()
                .ToList();

            var higherTaxaCount = matchingHigherTaxa.Count();
            switch (higherTaxaCount)
            {
                case 0:
                    this.logger?.Log(LogType.Warning, "Zero matches for rank {0}.", rank);
                    break;

                case 1:
                    {
                        string taxonName = matchingHigherTaxa.Single();

                        this.logger?.Log("{0}: {1}\t--\t{2}", genus, rank, taxonName);

                        string xpath = string.Format(TreatmentMetaReplaceXPathTemplate, genus, rank.MapTaxonRankTypeToTaxonRankString());
                        document.SelectNodes(xpath)
                            .AsParallel()
                            .ForAll(node =>
                            {
                                node.InnerText = taxonName;
                            });
                    }

                    break;

                default:
                    {
                        this.logger?.Log(LogType.Warning, "Multiple matches for rank {0}:", rank);
                        foreach (string taxonName in matchingHigherTaxa)
                        {
                            this.logger?.Log("{0}: {1}\t--\t{2}", genus, rank, taxonName);
                        }

                        this.logger?.Log();
                    }

                    break;
            }
        }
    }
}
