// <copyright file="ExtractHcmrResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml
{
    using System.Xml.Serialization;

    /// <summary>
    /// Extract HCMR response model.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "Reflect")]
    [XmlRoot(ElementName = "GetEntitiesResponse", Namespace = "Reflect", IsNullable = false)]
    public class ExtractHcmrResponseModel
    {
        /// <summary>
        /// Gets or sets items.
        /// </summary>
        [XmlArray(ElementName = "items", Namespace = "Reflect", IsNullable = false)]
        [XmlArrayItem(ElementName = "item", Namespace = "Reflect", IsNullable = false)]
        public Item[] Items { get; set; }
    }
}