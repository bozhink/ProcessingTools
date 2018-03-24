// <copyright file="HashParameters.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash Parameters.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "parameters", Namespace = "", IsNullable = false)]
    public class HashParameters
    {
        /// <summary>
        /// Gets or sets with-context.
        /// </summary>
        [XmlElement("with-context")]
        public HashParametersWithContext WithContext { get; set; }

        /// <summary>
        /// Gets or sets header-only.
        /// </summary>
        [XmlElement("header-only")]
        public HashParametersHeaderOnly HeaderOnly { get; set; }

        /// <summary>
        /// Gets or sets with-canonical-ranks.
        /// </summary>
        [XmlElement("with-canonical-ranks")]
        public HashParametersWithCanonicalRanks WithCanonicalRanks { get; set; }

        /// <summary>
        /// Gets or sets with-vernaculars.
        /// </summary>
        [XmlElement("with-vernaculars")]
        public HashParametersWithVernaculars WithVernaculars { get; set; }

        /// <summary>
        /// Gets or sets best-match-only.
        /// </summary>
        [XmlElement("best-match-only")]
        public HashParametersBestMatchOnly BestMatchOnly { get; set; }

        /// <summary>
        /// Gets or sets data-sources.
        /// </summary>
        [XmlElement("data-sources")]
        public HashParametersDataSources DataSources { get; set; }

        /// <summary>
        /// Gets or sets preferred-data-sources.
        /// </summary>
        [XmlElement("preferred-data-sources")]
        public HashParametersPreferredDataSources PreferredDataSources { get; set; }

        /// <summary>
        /// Gets or sets resolve-once.
        /// </summary>
        [XmlElement("resolve-once")]
        public HashParametersResolveOnce ResolveOnce { get; set; }
    }
}
