// <copyright file="IInstitutionLabel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories institution label.
    /// </summary>
    public interface IInstitutionLabel
    {
        /// <summary>
        /// Gets city or town.
        /// </summary>
        string CityTown { get; }

        /// <summary>
        /// Gets country.
        /// </summary>
        string Country { get; }

        /// <summary>
        /// Gets name of institution.
        /// </summary>
        string NameOfInstitution { get; }

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
