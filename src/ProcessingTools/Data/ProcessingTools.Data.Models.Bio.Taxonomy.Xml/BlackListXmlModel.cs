// <copyright file="BlackListXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Black list XML model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = XmlModelsConstants.BlackListXmlRootNodeName)]
    public class BlackListXmlModel
    {
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        [XmlElement(XmlModelsConstants.BlackListXmlItemElementName)]
        public string[] Items { get; set; }
    }
}
