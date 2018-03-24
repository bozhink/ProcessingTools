// <copyright file="DistrictsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

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
