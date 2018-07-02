// <copyright file="HashStatus.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Status.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "status", Namespace = "", IsNullable = false)]
    public class HashStatus
    {
        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
