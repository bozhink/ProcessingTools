// <copyright file="HashDataDatumResultsResult.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Data Datum Results Result.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "result", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResult
    {
        /// <summary>
        /// Gets or sets data-source-id.
        /// </summary>
        [XmlElement("data-source-id")]
        public HashDataDatumResultsResultDataSourceId DataSourceId { get; set; }

        /// <summary>
        /// Gets or sets data-source-title.
        /// </summary>
        [XmlElement("data-source-title")]
        public HashDataDatumResultsResultDataSourceTitle DataSourceTitle { get; set; }

        /// <summary>
        /// Gets or sets gni-uuid.
        /// </summary>
        [XmlElement("gni-uuid")]
        public HashDataDatumResultsResultGniUuid GniUuid { get; set; }

        /// <summary>
        /// Gets or sets name-string.
        /// </summary>
        [XmlElement("name-string")]
        public HashDataDatumResultsResultNameString NameString { get; set; }

        /// <summary>
        /// Gets or sets canonical-form.
        /// </summary>
        [XmlElement("canonical-form")]
        public HashDataDatumResultsResultCanonicalForm CanonicalForm { get; set; }

        /// <summary>
        /// Gets or sets classification-path.
        /// </summary>
        [XmlElement("classification-path")]
        public HashDataDatumResultsResultClassificationPath ClassificationPath { get; set; }

        /// <summary>
        /// Gets or sets classification-path-ranks.
        /// </summary>
        [XmlElement("classification-path-ranks")]
        public HashDataDatumResultsResultClassificationPathRanks ClassificationPathRanks { get; set; }

        /// <summary>
        /// Gets or sets classification-path-ids.
        /// </summary>
        [XmlElement("classification-path-ids")]
        public HashDataDatumResultsResultClassificationPathIds ClassificationPathIds { get; set; }

        /// <summary>
        /// Gets or sets taxon-id.
        /// </summary>
        [XmlElement("taxon-id")]
        public HashDataDatumResultsResultTaxonId TaxonId { get; set; }

        /// <summary>
        /// Gets or sets local-id.
        /// </summary>
        [XmlElement("local-id")]
        public HashDataDatumResultsResultLocalId LocalId { get; set; }

        /// <summary>
        /// Gets or sets global-id.
        /// </summary>
        [XmlElement("global-id")]
        public HashDataDatumResultsResultGlobalId GlobalId { get; set; }

        /// <summary>
        /// Gets or sets edit-distance.
        /// </summary>
        [XmlElement("edit-distance")]
        public HashDataDatumResultsResultEditDistance EditDistance { get; set; }

        /// <summary>
        /// Gets or sets current-taxon-id.
        /// </summary>
        [XmlElement("current-taxon-id")]
        public HashDataDatumResultsResultCurrentTaxonId CurrentTaxonId { get; set; }

        /// <summary>
        /// Gets or sets current-name-string.
        /// </summary>
        [XmlElement("current-name-string")]
        public HashDataDatumResultsResultCurrentNameString CurrentNameString { get; set; }

        /// <summary>
        /// Gets or sets url.
        /// </summary>
        [XmlElement("url")]
        public HashDataDatumResultsResultUrl Url { get; set; }

        /// <summary>
        /// Gets or sets match-type.
        /// </summary>
        [XmlElement("match-type")]
        public HashDataDatumResultsResultMatchType MatchType { get; set; }

        /// <summary>
        /// Gets or sets prescore.
        /// </summary>
        [XmlElement("prescore")]
        public HashDataDatumResultsResultPrescore Prescore { get; set; }

        /// <summary>
        /// Gets or sets score.
        /// </summary>
        [XmlElement("score")]
        public HashDataDatumResultsResultScore Score { get; set; }
    }
}
