// <copyright file="Entity.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml
{
    using System.Xml.Serialization;

    /// <summary>
    /// Entity.
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "Reflect")]
    [XmlRoot(ElementName = "entity", Namespace = "Reflect", IsNullable = false)]
    public class Entity
    {
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        [XmlElement(ElementName = "type", Namespace = "Reflect", IsNullable = false)]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets identifier.
        /// </summary>
        [XmlElement(ElementName = "identifier", Namespace = "Reflect", IsNullable = false)]
        public string Identifier { get; set; }
    }
}
