// <copyright file="IStaffLabel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories staff label.
    /// </summary>
    public interface IStaffLabel
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
        /// Gets postal or ZIP code.
        /// </summary>
        string PostalZipCode { get; }

        /// <summary>
        /// Gets primary institution.
        /// </summary>
        string PrimaryInstitution { get; }

        /// <summary>
        /// Gets staff member full name.
        /// </summary>
        string StaffMemberFullName { get; }

        /// <summary>
        /// Gets state or province.
        /// </summary>
        string StateProvince { get; }
    }
}
