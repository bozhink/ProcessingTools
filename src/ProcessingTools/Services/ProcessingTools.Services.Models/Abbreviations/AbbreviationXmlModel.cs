﻿// <copyright file="AbbreviationXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Abbreviations
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Services.Models.Contracts.Abbreviations;

    /// <summary>
    /// Abbreviation XML model.
    /// </summary>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.AbbreviationsNamespace)]
    [XmlRoot(ElementName = "abbreviation", Namespace = Namespaces.AbbreviationsNamespace, IsNullable = false)]
    public class AbbreviationXmlModel : IAbbreviationModel
    {
        /// <inheritdoc/>
        [XmlAttribute("content-type")]
        public string ContentType { get; set; }

        /// <inheritdoc/>
        [XmlElement("definition")]
        public string Definition { get; set; }

        /// <inheritdoc/>
        [XmlElement("value")]
        public string Value { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (this.ContentType + this.Value + this.Definition).GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}
