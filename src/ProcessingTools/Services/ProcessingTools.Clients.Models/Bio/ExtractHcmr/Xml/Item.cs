// <copyright file="Item.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml
{
    using System.Xml.Serialization;

    /// <summary>
    /// Item
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "Reflect")]
    [XmlRoot(ElementName = "item", Namespace = "Reflect", IsNullable = false)]
    public class Item
    {
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [XmlElement(ElementName = "name", Namespace = "Reflect", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets count.
        /// </summary>
        [XmlElement(ElementName = "count", Namespace = "Reflect", IsNullable = false)]
        public byte Count { get; set; }

        /// <summary>
        /// Gets or sets entities.
        /// </summary>
        [XmlArray(ElementName = "entities", Namespace = "Reflect", IsNullable = false)]
        [XmlArrayItem(ElementName = "entity", Namespace = "Reflect", IsNullable = false)]
        public Entity[] Entities { get; set; }
    }
}
