// <copyright file="HashParametersWithContext.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Parameters With Context.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "with-context", Namespace = "", IsNullable = false)]
    public class HashParametersWithContext
    {
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether context is applied.
        /// </summary>
        [XmlText]
        public bool Value { get; set; }
    }
}
