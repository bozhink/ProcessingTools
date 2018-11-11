// <copyright file="TaxonXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon XML model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListTaxonXmlModelElementName)]
    public class TaxonXmlModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether taxon name is part of the white-list.
        /// </summary>
        [XmlAttribute(XmlModelsConstants.RankListIsWhiteListedXmlAttributeName)]
        public bool IsWhiteListed { get; set; }

        /// <summary>
        /// Gets or sets the taxon parts.
        /// </summary>
        [XmlElement(XmlModelsConstants.RankListTaxonXmlPartElementName)]
        public TaxonPartXmlModel[] Parts { get; set; }
    }
}
