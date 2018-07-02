// <copyright file="HashParametersResolveOnce.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Parameters Resolve Once.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "resolve-once", Namespace = "", IsNullable = false)]
    public class HashParametersResolveOnce
    {
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether resolve once is applied.
        /// </summary>
        [XmlText]
        public bool Value { get; set; }
    }
}
