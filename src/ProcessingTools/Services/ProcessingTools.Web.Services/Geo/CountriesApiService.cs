// <copyright file="CountriesApiService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Geo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Services.Contracts.Geo;
    using ProcessingTools.Web.Models.Geo.Countries;
    using ProcessingTools.Web.Services.Contracts.Geo;

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
        /// <param name="service">Instance of <see cref="ICountriesDataService"/></param>
        public CountriesApiService(ICountriesDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ICountry, CountryResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
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
