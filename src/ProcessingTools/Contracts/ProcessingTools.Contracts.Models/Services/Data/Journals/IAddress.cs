// <copyright file="IAddress.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Services.Data.Journals
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Address.
    /// </summary>
    public interface IAddress : IStringIdentified, ProcessingTools.Contracts.Models.IAddressed, IServiceModel
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
