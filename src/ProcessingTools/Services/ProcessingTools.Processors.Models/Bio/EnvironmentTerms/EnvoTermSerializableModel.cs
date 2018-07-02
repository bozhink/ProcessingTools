// <copyright file="EnvoTermSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.EnvironmentTerms
{
    using System.Xml.Serialization;

    /// <summary>
    /// ENVO term serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot("envo", Namespace = "", IsNullable = false)]
    public class EnvoTermSerializableModel
    {
        /// <summary>
        /// Gets or sets @EnvoID.
        /// </summary>
        [XmlAttribute("EnvoID")]
        public string EnvoId { get; set; }

        /// <summary>
        /// Gets or sets @ID.
        /// </summary>
        [XmlAttribute("ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the XML text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets @VerbatimTerm.
        /// </summary>
        [XmlAttribute("VerbatimTerm")]
        public string VerbatimTerm { get; set; }
    }
}
