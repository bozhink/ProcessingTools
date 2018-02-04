// <copyright file="CountriesApiService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Geo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
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
        /// <param name="service">Instance of <see cref="ICountriesDataService"/></param>
        public CountriesApiService(ICountriesDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ICountry, CountryResponseModel>().ConstructUsing(i => new CountryResponseModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    LanguageCode = i.LanguageCode
                });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<CountryResponseModel[]> GetAllAsync()
        {
            var items = await this.service.SelectAsync(null).ConfigureAwait(false);
            if (items == null || !items.Any())
            {
                return new CountryResponseModel[] { };
            }

            return items.Select(this.mapper.Map<ICountry, CountryResponseModel>).ToArray();
        }
    }
}
