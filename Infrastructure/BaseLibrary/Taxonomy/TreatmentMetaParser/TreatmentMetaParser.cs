namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
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
                .Distinct()
                .ToArray();

            var response = await this.service.Resolve(genusList);

            foreach (string genus in genusList)
            {
                this.logger?.Log("\n{0}\n", genus);

                var classification = response.Where(r => r.Genus == genus);

                this.ReplaceTreatmentMetaClassificationItem(
                    classification
                        .Select(c => c.Kingdom)
                        .ToList(),
                    genus,
                    TaxonRankType.Kingdom);

                this.ReplaceTreatmentMetaClassificationItem(
                    classification
                        .Select(c => c.Order)
                        .ToList(),
                    genus,
                    TaxonRankType.Order);

                this.ReplaceTreatmentMetaClassificationItem(
                    classification
                        .Select(c => c.Family)
                        .ToList(),
                    genus,
                    TaxonRankType.Family);
            }
        }

        private void ReplaceTreatmentMetaClassificationItem(
            IEnumerable<string> matchingHigherTaxa,
            string genus,
            TaxonRankType rank)
        {
            this.ReplaceTreatmentMetaClassificationItem(matchingHigherTaxa, genus, rank.ToString().ToLower());
        }

        private void ReplaceTreatmentMetaClassificationItem(
            IEnumerable<string> matchingHigherTaxa,
            string genus,
            string rank)
        {
            if (matchingHigherTaxa == null)
            {
                throw new ArgumentNullException(nameof(matchingHigherTaxa));
            }

            int higherTaxaCount = matchingHigherTaxa.Count();

            switch (higherTaxaCount)
            {
                case 0:
                    this.logger?.Log("WARNING: Zero matches for rank {0}.", rank);
                    break;

                case 1:
                    {
                        string taxonName = matchingHigherTaxa.First();

                        this.logger?.Log("{0}: {1}\t--\t{2}", genus, rank, taxonName);

                        string xpath = string.Format(TreatmentMetaReplaceXPathTemplate, genus, rank);
                        foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                        {
                            node.InnerText = taxonName;
                        }
                    }

                    break;

                default:
                    {
                        this.logger?.Log("WARNING: Multiple for rank {0}:", rank);
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