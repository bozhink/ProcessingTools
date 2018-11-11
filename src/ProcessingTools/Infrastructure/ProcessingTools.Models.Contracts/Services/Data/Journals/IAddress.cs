// <copyright file="IAddress.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Services.Data.Journals
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Address.
    /// </summary>
    public interface IAddress : IStringIdentifiable, ProcessingTools.Models.Contracts.IAddressable, IServiceModel
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
