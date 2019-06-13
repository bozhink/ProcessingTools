﻿// <copyright file="IContinent.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Continent.
    /// </summary>
    public interface IContinent : IGeoSynonymisable<IContinentSynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets collection of countries.
        /// </summary>
        ICollection<ICountry> Countries { get; }
    }
}
