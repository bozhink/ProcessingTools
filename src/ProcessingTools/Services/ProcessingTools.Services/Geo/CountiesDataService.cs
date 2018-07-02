// <copyright file="CountiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

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
