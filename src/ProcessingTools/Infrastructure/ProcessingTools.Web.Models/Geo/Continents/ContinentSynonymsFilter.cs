// <copyright file="ContinentSynonymsFilter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Continents
{
    using ProcessingTools.Contracts.Filters.Geo;

    /// <summary>
    /// Continent synonyms filter
    /// </summary>
    public class ContinentSynonymsFilter : IContinentSynonymsFilter
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the synonym object.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the synonym object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        public int? LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the ID of the parent object.
        /// </summary>
        public int? ParentId { get; set; }
    }
}
