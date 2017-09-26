// <copyright file="IAddress.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Address.
    /// </summary>
    public interface IAddress : IGuidIdentifiable, IModelWithUserInformation
    {
        /// <summary>
        /// Gets address string.
        /// </summary>
        string AddressString { get; }

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
