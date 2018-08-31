// <copyright file="IContinent.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Continent.
    /// </summary>
    public interface IContinent : IGeoSynonymisable<IContinentSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        /// <summary>
        /// Gets collection of countries.
        /// </summary>
        ICollection<ICountry> Countries { get; }
    }
}
