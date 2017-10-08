// <copyright file="HashDataDatum.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash data datum.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "datum", Namespace = "", IsNullable = false)]
    public class HashDataDatum
    {

        /// <summary>
        /// Gets or sets supplied-name-string.
        /// </summary>
        [XmlElement("supplied-name-string")]
        public HashDataDatumSuppliedNameString SuppliedNameString { get; set; }

        /// <summary>
        /// Gets or sets is-known-name.
        /// </summary>
        [XmlElement("is-known-name")]
        public HashDataDatumIsKnownName IsKnownName { get; set; }

        /// <summary>
        /// Gets or sets results.
        /// </summary>
        [XmlElement("results")]
        public HashDataDatumResults Results { get; set; }
    }
}
