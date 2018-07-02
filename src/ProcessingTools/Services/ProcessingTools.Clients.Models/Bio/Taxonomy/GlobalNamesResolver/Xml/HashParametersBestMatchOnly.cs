// <copyright file="HashParametersBestMatchOnly.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Parameters Best Match Only.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "best-match-only", Namespace = "", IsNullable = false)]
    public class HashParametersBestMatchOnly
    {
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether best match only is applied.
        /// </summary>
        [XmlText]
        public bool Value { get; set; }
    }
}
