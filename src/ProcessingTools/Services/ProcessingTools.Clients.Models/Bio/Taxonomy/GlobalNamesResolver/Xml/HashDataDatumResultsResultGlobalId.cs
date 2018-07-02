// <copyright file="HashDataDatumResultsResultGlobalId.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Data Datum Results Result Global Id.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "global-id", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultGlobalId
    {
        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
