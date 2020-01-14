// <copyright file="ProvincesDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

    /// <summary>
    /// Provinces data service.
    /// </summary>
    public class ProvincesDataService : AbstractGeoSynonymisableDataService<IProvincesRepository, IProvince, IProvincesFilter, IProvinceSynonym, IProvinceSynonymsFilter>, IProvincesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvincesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IProvincesRepository"/>.</param>
        public ProvincesDataService(IProvincesRepository repository)
            : base(repository)
        {
        }
    }
}
