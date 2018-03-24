// <copyright file="ICounty.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// County.
    /// </summary>
    public interface ICounty : IGeoSynonymisable<ICountySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        /// <summary>
        /// Gets country ID.
        /// </summary>
        int CountryId { get; }

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
        /// Gets region ID.
        /// </summary>
        int? DistrictId { get; }

        /// <summary>
        /// Gets municipality ID.
        /// </summary>
        int? MunicipalityId { get; }

        /// <summary>
        /// Gets cities.
        /// </summary>
        ICollection<ICity> Cities { get; }
    }
}
