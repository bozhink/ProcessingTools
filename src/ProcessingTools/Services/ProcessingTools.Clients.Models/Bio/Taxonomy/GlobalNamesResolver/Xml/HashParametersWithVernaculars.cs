﻿// <copyright file="HashParametersWithVernaculars.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Parameters With Vernaculars.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "with-vernaculars", Namespace = "", IsNullable = false)]
    public class HashParametersWithVernaculars
    {
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether vernaculars should be returned.
        /// </summary>
        [XmlText]
        public bool Value { get; set; }
    }
}
