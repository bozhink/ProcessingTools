// <copyright file="ICity.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// City.
    /// </summary>
    public interface ICity : IGeoSynonymisable<ICitySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        /// <summary>
        /// Gets country ID.
        /// </summary>
        int CountryId { get; }

        /// <summary>
        /// Gets country.
        /// </summary>
        ICountry Country { get; }

        /// <summary>
        /// Gets state ID.
        /// </summary>
        int? StateId { get; }

        /// <summary>
        /// Gets province ID.
        /// </summary>
        int? ProvinceId { get; }

        /// <summary>
        /// Gets region ID.
        /// </summary>
        int? RegionId { get; }

        /// <summary>
        /// Gets district ID.
        /// </summary>
        int? DistrictId { get; }

        /// <summary>
        /// Gets municipality ID.
        /// </summary>
        int? MunicipalityId { get; }

        /// <summary>
        /// Gets county ID.
        /// </summary>
        int? CountyId { get; }

        /// <summary>
        /// Gets collection of post codes.
        /// </summary>
        ICollection<IPostCode> PostCodes { get; }
    }
}
