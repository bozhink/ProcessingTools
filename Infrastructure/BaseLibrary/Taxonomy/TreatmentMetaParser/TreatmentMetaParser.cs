namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.DocumentProvider;

    public class TreatmentMetaParser : TaxPubDocument, IParser
    {
        private const string SelectTreatmentGeneraXPathString = "//tp:taxon-treatment[string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'])='ORDO' or string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'])='FAMILIA']/tp:nomenclature/tn/tn-part[@type='genus']";

        private const string TreatmentMetaReplaceXPathTemplate = "//tp:taxon-treatment[string(tp:nomenclature/tn/tn-part[@type='genus'])='{0}' or string(tp:nomenclature/tn/tn-part[@type='genus']/@full-name)='{0}']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='{1}']";

        private ILogger logger;

        private ITaxaInformationResolverDataService<ITaxonClassification> service;

        public TreatmentMetaParser(ITaxaInformationResolverDataService<ITaxonClassification> service, string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
            this.service = service;
        }

        public async Task Parse()
        {
            var genusList = this.XmlDocument.SelectNodes(SelectTreatmentGeneraXPathString, this.NamespaceManager)
                .Cast<XmlNode>()
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

                    this.ReplaceTreatmentMetaClassificationItem(classification, genus, TaxonRankType.Kingdom);

                    this.ReplaceTreatmentMetaClassificationItem(classification, genus, TaxonRankType.Order);

                    this.ReplaceTreatmentMetaClassificationItem(classification, genus, TaxonRankType.Family);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e);
                }
            }
        }

        private Expression<Func<ITaxonClassification, string>> GetSelectorForTaxonRank(TaxonRankType rank)
        {
            Expression<Func<ITaxonClassification, string>> result = c => c.Classification.SingleOrDefault(x => x.Rank == rank).ScientificName;
            return result;
        }

        private void ReplaceTreatmentMetaClassificationItem(IQueryable<ITaxonClassification> classification, string genus, TaxonRankType rank)
        {
            if (classification == null)
            {
                throw new ArgumentNullException(nameof(classification));
            }

            var matchingHigherTaxa = classification
                .Select(this.GetSelectorForTaxonRank(rank))
                .Distinct()
                .ToList();

            int higherTaxaCount = matchingHigherTaxa.Count();

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
                        foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                        {
                            node.InnerText = taxonName;
                        }
                    }

                    break;

                default:
                    {
                        this.logger?.Log(LogType.Warning, "Multiple for rank {0}:", rank);
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