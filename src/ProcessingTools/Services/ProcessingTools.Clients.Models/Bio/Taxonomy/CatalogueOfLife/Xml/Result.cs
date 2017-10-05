// <copyright file="Result.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Result.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "result")]
    public class Result : AcceptedName
    {
        /// <summary>
        /// Gets or sets accepted name.
        /// </summary>
        [XmlElement("accepted_name", typeof(Result))]
        public Result AcceptedName { get; set; }
    }
}
