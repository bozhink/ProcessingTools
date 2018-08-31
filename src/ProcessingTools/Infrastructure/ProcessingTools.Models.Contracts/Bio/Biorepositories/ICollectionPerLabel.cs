// <copyright file="ICollectionPerLabel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories CollectionPerLabel
    /// </summary>
    public interface ICollectionPerLabel
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
        /// Gets state or province.
        /// </summary>
        string StateProvince { get; }
    }
}
