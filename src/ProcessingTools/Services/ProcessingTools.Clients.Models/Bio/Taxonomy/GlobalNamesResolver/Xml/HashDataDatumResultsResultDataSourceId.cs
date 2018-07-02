// <copyright file="HashDataDatumResultsResultDataSourceId.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Data Datum Results Result Data Source Id.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "data-source-id", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultDataSourceId
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
        public int Value { get; set; }
    }
}
