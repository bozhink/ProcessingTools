namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "result", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResult
    {
        [XmlElement("data-source-id")]
        public HashDataDatumResultsResultDataSourceId DataSourceId { get; set; }

        [XmlElement("data-source-title")]
        public HashDataDatumResultsResultDataSourceTitle DataSourceTitle { get; set; }

        [XmlElement("gni-uuid")]
        public HashDataDatumResultsResultGniUuid GniUuid { get; set; }

        [XmlElement("name-string")]
        public HashDataDatumResultsResultNameString NameString { get; set; }

        [XmlElement("canonical-form")]
        public HashDataDatumResultsResultCanonicalForm CanonicalForm { get; set; }

        [XmlElement("classification-path")]
        public HashDataDatumResultsResultClassificationPath ClassificationPath { get; set; }

        [XmlElement("classification-path-ranks")]
        public HashDataDatumResultsResultClassificationPathRanks ClassificationPathRanks { get; set; }

        [XmlElement("classification-path-ids")]
        public HashDataDatumResultsResultClassificationPathIds ClassificationPathIds { get; set; }

        [XmlElement("taxon-id")]
        public HashDataDatumResultsResultTaxonId TaxonId { get; set; }

        [XmlElement("local-id")]
        public HashDataDatumResultsResultLocalId LocalId { get; set; }

        [XmlElement("global-id")]
        public HashDataDatumResultsResultGlobalId GlobalId { get; set; }

        [XmlElement("edit-distance")]
        public HashDataDatumResultsResultEditDistance EditDistance { get; set; }

        [XmlElement("current-taxon-id")]
        public HashDataDatumResultsResultCurrentTaxonId CurrentTaxonId { get; set; }

        [XmlElement("current-name-string")]
        public HashDataDatumResultsResultCurrentNameString CurrentNameString { get; set; }

        [XmlElement("url")]
        public HashDataDatumResultsResultUrl Url { get; set; }

        [XmlElement("match-type")]
        public HashDataDatumResultsResultMatchType MatchType { get; set; }

        [XmlElement("prescore")]
        public HashDataDatumResultsResultPrescore Prescore { get; set; }

        [XmlElement("score")]
        public HashDataDatumResultsResultScore Score { get; set; }
    }
}
