// <copyright file="IAddress.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Journals
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Address.
    /// </summary>
    public interface IAddress : IStringIdentified, ProcessingTools.Contracts.Models.IAddressed, IDataModel
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
