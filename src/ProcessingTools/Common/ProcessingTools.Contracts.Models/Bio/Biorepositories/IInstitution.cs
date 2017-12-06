// <copyright file="IInstitution.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Biorepositories
{
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Biorepositories institution.
    /// </summary>
    public interface IInstitution
    {
        /// <summary>
        /// Gets additional institution names.
        /// </summary>
        string AdditionalInstitutionNames { get; }

        /// <summary>
        /// Gets cites permit number.
        /// </summary>
        string CitesPermitNumber { get; }

        /// <summary>
        /// Gets cool URI.
        /// </summary>
        string CoolUri { get; }

        /// <summary>
        /// Gets date when herbarium was founded.
        /// </summary>
        string DateHerbariumWasFounded { get; }

        /// <summary>
        /// Gets description of institution.
        /// </summary>
        string DescriptionOfInstitution { get; }

        /// <summary>
        /// Gets geographic coverage of herbarium.
        /// </summary>
        string GeographicCoverageOfHerbarium { get; }

        /// <summary>
        /// Gets incorporated herbaria.
        /// </summary>
        string IncorporatedHerbaria { get; }

        /// <summary>
        /// Gets whether is Index Herbariorum record.
        /// </summary>
        IndexHerbariorumRecordType IndexHerbariorumRecord { get; }

        /// <summary>
        /// Gets institutional discipline.
        /// </summary>
        string InstitutionalDiscipline { get; }

        /// <summary>
        /// Gets institutional governance.
        /// </summary>
        string InstitutionalGovernance { get; }

        /// <summary>
        /// Gets institutional LSID.
        /// </summary>
        string InstitutionalLsid { get; }

        /// <summary>
        /// Gets institution code.
        /// </summary>
        string InstitutionCode { get; }

        /// <summary>
        /// Gets institution name.
        /// </summary>
        string InstitutionName { get; }

        /// <summary>
        /// Gets institution type.
        /// </summary>
        string InstitutionType { get; }

        /// <summary>
        /// Gets number of specimens in herbarium.
        /// </summary>
        string NumberOfSpecimensInHerbarium { get; }

        /// <summary>
        /// Gets primary contact.
        /// </summary>
        string PrimaryContact { get; }

        /// <summary>
        /// Gets serials published by institution.
        /// </summary>
        string SerialsPublishedByInstitution { get; }

        /// <summary>
        /// Gets status of institution.
        /// </summary>
        string StatusOfInstitution { get; }

        /// <summary>
        /// Gets taxonomic coverage of herbarium.
        /// </summary>
        string TaxonomicCoverageOfHerbarium { get; }

        /// <summary>
        /// Gets URL.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets URL for institutional specimen catalog.
        /// </summary>
        string UrlForInstitutionalSpecimenCatalog { get; }

        /// <summary>
        /// Gets URL for institutional web services.
        /// </summary>
        string UrlForInstitutionalWebservices { get; }

        /// <summary>
        /// Gets URL for main institutional web site.
        /// </summary>
        string UrlForMainInstitutionalWebsite { get; }
    }
}
