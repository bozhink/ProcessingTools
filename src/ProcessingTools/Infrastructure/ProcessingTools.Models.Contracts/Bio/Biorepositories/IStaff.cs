// <copyright file="IStaff.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories staff.
    /// </summary>
    public interface IStaff
    {
        /// <summary>
        /// Gets additional affiliations.
        /// </summary>
        string AdditionalAffiliations { get; }

        /// <summary>
        /// Gets area of responsibility.
        /// </summary>
        string AreaOfResponsibility { get; }

        /// <summary>
        /// Gets birth year.
        /// </summary>
        string BirthYear { get; }

        /// <summary>
        /// Gets city or town.
        /// </summary>
        string CityTown { get; }

        /// <summary>
        /// Gets country.
        /// </summary>
        string Country { get; }

        /// <summary>
        /// Gets e-mail.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets fax number.
        /// </summary>
        string FaxNumber { get; }

        /// <summary>
        /// Gets first name.
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Gets job title.
        /// </summary>
        string JobTitle { get; }

        /// <summary>
        /// Gets last name.
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Gets phone number.
        /// </summary>
        string PhoneNumber { get; }

        /// <summary>
        /// Gets postal or ZI code.
        /// </summary>
        string PostalZipCode { get; }

        /// <summary>
        /// Gets primary collection.
        /// </summary>
        string PrimaryCollection { get; }

        /// <summary>
        /// Gets primary institution.
        /// </summary>
        string PrimaryInstitution { get; }

        /// <summary>
        /// Gets research discipline.
        /// </summary>
        string ResearchDiscipline { get; }

        /// <summary>
        /// Gets research specialty.
        /// </summary>
        string ResearchSpecialty { get; }

        /// <summary>
        /// Gets staff member full name.
        /// </summary>
        string StaffMemberFullName { get; }

        /// <summary>
        /// Gets state or province.
        /// </summary>
        string StateProvince { get; }

        /// <summary>
        /// Gets URL.
        /// </summary>
        string Url { get; }
    }
}
