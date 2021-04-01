// <copyright file="CountriesApiService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Web.Models.Geo.Countries;

    /// <summary>
    /// Default implementation of <see cref="ICountriesApiService"/>.
    /// </summary>
    public class CountriesApiService : ICountriesApiService
    {
        private readonly ICountriesDataService service;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesApiService"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ICountriesDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public CountriesApiService(ICountriesDataService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<IList<CountryResponseModel>> GetAllAsync()
        {
            var items = await this.service.SelectAsync(null).ConfigureAwait(false);
            if (items == null || !items.Any())
            {
                return Array.Empty<CountryResponseModel>();
            }

            return items.Select(this.mapper.Map<ICountry, CountryResponseModel>).ToArray();
        }
    }
}
