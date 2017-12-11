// <copyright file="ICollectionPer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories Collection Per.
    /// </summary>
    public interface ICollectionPer
    {
        /// <summary>
        /// Gets access eligibility and rules.
        /// </summary>
        string AccessEligibilityAndRules { get; }

        /// <summary>
        /// Gets collection code.
        /// </summary>
        string CollectionCode { get; }

        /// <summary>
        /// Gets collection content type.
        /// </summary>
        string CollectionContentType { get; }

        /// <summary>
        /// Gets collection description.
        /// </summary>
        string CollectionDescription { get; }

        /// <summary>
        /// Gets collection name.
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        /// Gets cool URI.
        /// </summary>
        string CoolUri { get; }

        /// <summary>
        /// Gets institution name.
        /// </summary>
        string InstitutionName { get; }

        /// <summary>
        /// Gets LSID.
        /// </summary>
        string Lsid { get; }

        /// <summary>
        /// Gets preservation type.
        /// </summary>
        string PreservationType { get; }

        /// <summary>
        /// Gets primary contact.
        /// </summary>
        string PrimaryContact { get; }

        /// <summary>
        /// Gets status of collection.
        /// </summary>
        string StatusOfCollection { get; }

        /// <summary>
        /// Gets URL.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets URL for collection.
        /// </summary>
        string UrlForCollection { get; }

        /// <summary>
        /// Gets URL for collection specimen catalog.
        /// </summary>
        string UrlForCollectionSpecimenCatalog { get; }

        /// <summary>
        /// Gets URL for collection web services.
        /// </summary>
        string UrlForCollectionWebservices { get; }
    }
}
