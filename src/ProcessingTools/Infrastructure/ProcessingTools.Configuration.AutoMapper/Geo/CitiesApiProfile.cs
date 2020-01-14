// <copyright file="CitiesApiProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Geo
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Web.Models.Geo.Cities;

    /// <summary>
    /// Cities (API) profile.
    /// </summary>
    public class CitiesApiProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitiesApiProfile"/> class.
        /// </summary>
        public CitiesApiProfile()
        {
            this.CreateMap<ICountry, CountryResponseModel>();
            this.CreateMap<ICity, CityResponseModel>().ConstructUsing(i => new CityResponseModel
            {
                Id = i.Id,
                Name = i.Name,
                Country = new CountryResponseModel
                {
                    Id = i.Country.Id,
                    Name = i.Country.Name,
                },
            });
        }
    }
}
