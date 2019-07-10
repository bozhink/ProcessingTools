// <copyright file="MunicipalitiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Geo;

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

    /// <summary>
    /// Municipalities data service.
    /// </summary>
    public class MunicipalitiesDataService : AbstractGeoSynonymisableDataService<IMunicipalitiesRepository, IMunicipality, IMunicipalitiesFilter, IMunicipalitySynonym, IMunicipalitySynonymsFilter>, IMunicipalitiesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MunicipalitiesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IMunicipalitiesRepository"/>.</param>
        public MunicipalitiesDataService(IMunicipalitiesRepository repository)
            : base(repository)
        {
        }
    }
}
