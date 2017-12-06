// <copyright file="IAddress.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Models.Data.Journals
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Address.
    /// </summary>
    public interface IAddress : IStringIdentifiable, ProcessingTools.Contracts.Models.IAddressable, IServiceModel
    {
        /// <summary>
        /// Gets City ID.
        /// </summary>
        int? CityId { get; }

        /// <summary>
        /// Gets Country ID.
        /// </summary>
        int? CountryId { get; }
    }
}
