﻿// <copyright file="RegionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

    /// <summary>
    /// Regions data service.
    /// </summary>
    public class RegionsDataService : AbstractGeoSynonymisableDataService<IRegionsRepository, IRegion, IRegionsFilter, IRegionSynonym, IRegionSynonymsFilter>, IRegionsDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionsDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IRegionsRepository"/>.</param>
        public RegionsDataService(IRegionsRepository repository)
            : base(repository)
        {
        }
    }
}
