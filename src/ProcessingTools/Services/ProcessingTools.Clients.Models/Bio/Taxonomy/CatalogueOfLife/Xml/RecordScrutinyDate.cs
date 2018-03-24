// <copyright file="RecordScrutinyDate.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Record scrutiny date.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "record_scrutiny_date")]
    public partial class RecordScrutinyDate
    {
        /// <summary>
        /// Gets or sets scrutiny.
        /// </summary>
        [XmlElement("scrutiny")]
        public string Scrutiny { get; set; }
    }
}
