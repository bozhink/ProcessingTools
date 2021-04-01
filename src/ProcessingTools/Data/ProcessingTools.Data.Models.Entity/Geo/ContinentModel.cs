// <copyright file="ContinentModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Continent model.
    /// </summary>
    public class ContinentModel : IContinent
    {
        /// <summary>
        /// Gets or sets the abbreviated name of the continent.
        /// </summary>
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets the collection of countries.
        /// </summary>
        public ICollection<ICountry> Countries { get; private set; } = new HashSet<ICountry>();

        /// <summary>
        /// Gets or sets the ID of the continent.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the continent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of synonyms.
        /// </summary>
        public ICollection<IContinentSynonym> Synonyms { get; private set; } = new HashSet<IContinentSynonym>();
    }
}
