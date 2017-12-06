// <copyright file="ICollectionLabel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories collection label.
    /// </summary>
    public interface ICollectionLabel
    {
        /// <summary>
        /// Gets city or town.
        /// </summary>
        string CityTown { get; }

        /// <summary>
        /// Gets collection name.
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        /// Gets country.
        /// </summary>
        string Country { get; }

        /// <summary>
        /// Gets postal or ZIP code.
        /// </summary>
        string PostalZipCode { get; }

        /// <summary>
        /// Gets primary contact.
        /// </summary>
        string PrimaryContact { get; }

        /// <summary>
        /// Gets state province.
        /// </summary>
        string StateProvince { get; }
    }
}
