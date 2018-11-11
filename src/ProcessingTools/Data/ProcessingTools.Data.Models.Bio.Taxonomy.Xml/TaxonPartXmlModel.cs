// <copyright file="TaxonPartXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon part XML model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlPartElementName)]
    public class TaxonPartXmlModel
    {
        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartValueElementName)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets taxon ranks.
        /// </summary>
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartRankElementName)]
        public TaxonRankXmlModel Ranks { get; set; }
    }
}
