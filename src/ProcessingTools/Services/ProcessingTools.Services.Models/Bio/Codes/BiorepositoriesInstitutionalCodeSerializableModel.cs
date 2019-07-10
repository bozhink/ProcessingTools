﻿// <copyright file="BiorepositoriesInstitutionalCodeSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Codes
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Biorepositories institutional code serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.InstitutionalCode, Namespace = "", IsNullable = false)]
    public class BiorepositoriesInstitutionalCodeSerializableModel
    {
        /// <summary>
        /// Gets or sets @description.
        /// </summary>
        [XmlAttribute(AttributeNames.Description)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets @url.
        /// </summary>
        [XmlAttribute(AttributeNames.Url)]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the XML text value.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
