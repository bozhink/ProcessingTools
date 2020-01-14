// <copyright file="AbbreviationsXmlModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Abbreviations
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Abbreviations XML model.
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
