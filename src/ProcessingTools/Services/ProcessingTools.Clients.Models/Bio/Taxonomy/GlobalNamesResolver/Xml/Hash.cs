// <copyright file="Hash.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Hash.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "hash", Namespace = "", IsNullable = false)]
    public class Hash
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [XmlElement("id")]
        public HashId Id { get; set; }

        /// <summary>
        /// Gets or sets url.
        /// </summary>
        [XmlElement("url")]
        public HashUrl Url { get; set; }

        /// <summary>
        /// Gets or sets data-sources.
        /// </summary>
        [XmlElement("data-sources")]
        public HashDataSources DataSources { get; set; }

        /// <summary>
        /// Gets or sets data.
        /// </summary>
        [XmlElement("data")]
        public HashData Data { get; set; }

        /// <summary>
        /// Gets or sets status.
        /// </summary>
        [XmlElement("status")]
        public HashStatus Status { get; set; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        [XmlElement("message")]
        public HashMessage Message { get; set; }

        /// <summary>
        /// Gets or sets parameters.
        /// </summary>
        [XmlElement("parameters")]
        public HashParameters Parameters { get; set; }
    }
}
