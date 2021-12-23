// <copyright file="CountiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

    /// <summary>
    /// Counties data service.
    /// </summary>
    public class CountiesDataService : AbstractGeoSynonymisableDataService<ICountiesRepository, ICounty, ICountiesFilter, ICountySynonym, ICountySynonymsFilter>, ICountiesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountiesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="ICountiesRepository"/>.</param>
        public CountiesDataService(ICountiesRepository repository)
            : base(repository)
        {
        }
    }
}
