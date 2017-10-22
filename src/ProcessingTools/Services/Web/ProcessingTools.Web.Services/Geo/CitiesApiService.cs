// <copyright file="CitiesApiService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Geo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Data.Geo;
    using ProcessingTools.Web.Contracts.Services.Geo;
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
        /// <param name="service">Instance of <see cref="ICitiesDataService"/></param>
        public CitiesApiService(ICitiesDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ICity, CityResponseModel>().ConstructUsing(i => new CityResponseModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Country = new CountryResponseModel
                    {
                        Id = i.Country.Id,
                        Name = i.Country.Name
                    }
                });
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<CityResponseModel[]> GetAllAsync()
        {
            var items = await this.service.SelectAsync(null).ConfigureAwait(false);
            if (items == null || !items.Any())
            {
                return new CityResponseModel[] { };
            }

            return items.Select(this.mapper.Map<ICity, CityResponseModel>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<CityResponseModel> GetById(int id)
        {
            var item = await this.service.GetByIdAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return null;
            }

            return this.mapper.Map<ICity, CityResponseModel>(item);
        }
    }
}
