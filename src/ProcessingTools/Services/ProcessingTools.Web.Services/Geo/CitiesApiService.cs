// <copyright file="CitiesApiService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Geo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Contracts.Web.Services.Geo;
    using ProcessingTools.Web.Models.Geo.Cities;

    /// <summary>
    /// Default implementation of <see cref="ICitiesApiService"/>.
    /// </summary>
    public class CitiesApiService : ICitiesApiService
    {
        private readonly ICitiesDataService service;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CitiesApiService"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ICitiesDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public CitiesApiService(ICitiesDataService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<IList<CityResponseModel>> GetAllAsync()
        {
            var items = await this.service.SelectAsync(null).ConfigureAwait(false);
            if (items is null || !items.Any())
            {
                return Array.Empty<CityResponseModel>();
            }

            return items.Select(this.mapper.Map<ICity, CityResponseModel>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<CityResponseModel> GetById(int id)
        {
            var item = await this.service.GetByIdAsync(id).ConfigureAwait(false);
            if (item is null)
            {
                return null;
            }

            return this.mapper.Map<ICity, CityResponseModel>(item);
        }
    }
}
