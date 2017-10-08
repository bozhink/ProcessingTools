// <copyright file="HashParametersPreferredDataSources.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    // TODO: <preferred-data-sources type="array" />
    /// <summary>
    /// Hash Parameters Preferred Data Sources.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "preferred-data-sources", Namespace = "", IsNullable = false)]
    public class HashParametersPreferredDataSources
    {
        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
