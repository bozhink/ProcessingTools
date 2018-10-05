// <copyright file="TaxonRankXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank XML model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlPartRankElementName)]
    public class TaxonRankXmlModel
    {
        /// <summary>
        /// Gets or sets the taxon ranks as string values.
        /// </summary>
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartRankValueElementName)]
        public string[] Values { get; set; }
    }
}
