// <copyright file="HashDataDatumResultsResultClassificationPathIds.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Data Datum Results Result Classification Path Ids.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "classification-path-ids", Namespace = "", IsNullable = false)]
    public class HashDataDatumResultsResultClassificationPathIds
    {
        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
