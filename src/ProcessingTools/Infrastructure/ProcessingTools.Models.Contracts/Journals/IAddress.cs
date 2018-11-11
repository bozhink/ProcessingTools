// <copyright file="IAddress.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Journals
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Address.
    /// </summary>
    public interface IAddress : IStringIdentifiable, ProcessingTools.Models.Contracts.IAddressable, IDataModel
    {
        /// <summary>
        /// Gets city ID.
        /// </summary>
        int? CityId { get; }

        /// <summary>
        /// Gets country ID.
        /// </summary>
        int? CountryId { get; }
    }
}
