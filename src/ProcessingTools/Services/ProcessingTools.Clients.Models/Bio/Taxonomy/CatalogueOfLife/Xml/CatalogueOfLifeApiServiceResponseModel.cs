// <copyright file="CatalogueOfLifeApiServiceResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Catalogue of Life API service response.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "results")]
    public class CatalogueOfLifeApiServiceResponseModel
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets total number of results.
        /// </summary>
        [XmlAttribute("total_number_of_results")]
        public int TotalNumberOfResults { get; set; }

        /// <summary>
        /// Gets or sets number of results returned.
        /// </summary>
        [XmlAttribute("number_of_results_returned")]
        public int NumberOfResultsReturned { get; set; }

        /// <summary>
        /// Gets or sets start.
        /// </summary>
        [XmlAttribute("start")]
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        [XmlAttribute("error_message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets version.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets rank.
        /// </summary>
        [XmlAttribute("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets result.
        /// </summary>
        [XmlElement("result")]
        public Result[] Results { get; set; }
    }
}
