// <copyright file="DistrictsDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

    /// <summary>
    /// Districts data service.
    /// </summary>
    public class DistrictsDataService : AbstractGeoSynonymisableDataService<IDistrictsRepository, IDistrict, IDistrictsFilter, IDistrictSynonym, IDistrictSynonymsFilter>, IDistrictsDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictsDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IDistrictsRepository"/>.</param>
        public DistrictsDataService(IDistrictsRepository repository)
            : base(repository)
        {
        }
    }
}
