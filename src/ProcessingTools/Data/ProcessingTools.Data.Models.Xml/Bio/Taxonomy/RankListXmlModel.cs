// <copyright file="RankListXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Xml.Bio.Taxonomy
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Rank list XML model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.RankListXmlRootNodeName)]
    public class RankListXmlModel
    {
        /// <summary>
        /// Gets or sets taxa.
        /// </summary>
        [XmlElement(XmlModelsConstants.RankListTaxonXmlModelElementName)]
        public TaxonXmlModel[] Taxa { get; set; }
    }
}
