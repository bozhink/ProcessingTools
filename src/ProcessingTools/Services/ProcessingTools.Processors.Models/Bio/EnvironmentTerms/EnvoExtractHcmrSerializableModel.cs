// <copyright file="EnvoExtractHcmrSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.EnvironmentTerms
{
    using System.Xml.Serialization;

    /// <summary>
    /// ENVO EXTRACT HCMR serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot("envo", Namespace = "", IsNullable = false)]
    public class EnvoExtractHcmrSerializableModel
    {
        /// <summary>
        /// Gets or sets @identifier1.
        /// </summary>
        [XmlAttribute("identifier1")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets @type1.
        /// </summary>
        [XmlAttribute("type1")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the XML text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
