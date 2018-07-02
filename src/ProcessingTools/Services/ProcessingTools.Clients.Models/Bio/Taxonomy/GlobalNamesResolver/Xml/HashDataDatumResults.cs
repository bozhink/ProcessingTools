// <copyright file="HashDataDatumResults.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Data Datum Results.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "results", Namespace = "", IsNullable = false)]
    public class HashDataDatumResults
    {
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets result.
        /// </summary>
        [XmlElement("result")]
        public HashDataDatumResultsResult[] Result { get; set; }
    }
}
