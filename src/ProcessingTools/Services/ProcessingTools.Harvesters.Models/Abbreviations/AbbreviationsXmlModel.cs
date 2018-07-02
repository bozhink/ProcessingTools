// <copyright file="AbbreviationsXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Abbreviations
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    /// <summary>
    /// Abbreviations Xml Model
    /// </summary>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Namespaces.AbbreviationsNamespace)]
    [XmlRoot(ElementName = "abbreviations", Namespace = Namespaces.AbbreviationsNamespace, IsNullable = false)]
    public class AbbreviationsXmlModel
    {
        /// <summary>
        /// Gets or sets abbreviations.
        /// </summary>
        [XmlElement("abbreviation")]
        public AbbreviationXmlModel[] Abbreviations { get; set; }
    }
}
