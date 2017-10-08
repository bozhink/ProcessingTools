// <copyright file="HashParametersDataSources.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    // TODO: <data-sources type="array" />
    /// <summary>
    /// Hash Parameters Data Sources.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "data-sources", Namespace = "", IsNullable = false)]
    public class HashParametersDataSources
    {
        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
