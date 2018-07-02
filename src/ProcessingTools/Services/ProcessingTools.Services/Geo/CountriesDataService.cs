// <copyright file="CountriesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    /// <summary>
    /// Countries data service.
    /// </summary>
    public class CountriesDataService : AbstractGeoSynonymisableDataService<ICountriesRepository, ICountry, ICountriesFilter, ICountrySynonym, ICountrySynonymsFilter>, ICountriesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="ICountriesRepository"/>.</param>
        public CountriesDataService(ICountriesRepository repository)
            : base(repository)
        {
        }
    }
}
