// <copyright file="HashParametersWithCanonicalRanks.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Parameters With Canonical Ranks.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "with-canonical-ranks", Namespace = "", IsNullable = false)]
    public class HashParametersWithCanonicalRanks
    {
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        [XmlText]
        public bool Value { get; set; }
    }
}
