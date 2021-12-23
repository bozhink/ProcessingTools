// <copyright file="CountriesApiProfile.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Geo
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Web.Models.Geo.Countries;

    /// <summary>
    /// Countries (API) profile.
    /// </summary>
    public class CountriesApiProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesApiProfile"/> class.
        /// </summary>
        public CountriesApiProfile()
        {
            this.CreateMap<ICountry, CountryResponseModel>();
        }
    }
}
