namespace ProcessingTools.Contracts.Data.Bio.Biorepositories.Models
{
    using ProcessingTools.Enumerations;

    public interface IInstitution
    {
        string AdditionalInstitutionNames { get; }

        string CitesPermitNumber { get; }

        string CoolUri { get; }

        string DateHerbariumWasFounded { get; }

        string DescriptionOfInstitution { get; }

        string GeographicCoverageOfHerbarium { get; }

        string IncorporatedHerbaria { get; }

        IndexHerbariorumRecordType IndexHerbariorumRecord { get; }

        string InstitutionalDiscipline { get; }

        string InstitutionalGovernance { get; }

        string InstitutionalLsid { get; }

        string InstitutionCode { get; }

        string InstitutionName { get; }

        string InstitutionType { get; }

        string NumberOfSpecimensInHerbarium { get; }

        string PrimaryContact { get; }

        string SerialsPublishedByInstitution { get; }

        string StatusOfInstitution { get; }

        string TaxonomicCoverageOfHerbarium { get; }

        string Url { get; }

        string UrlForInstitutionalSpecimenCatalog { get; }

        string UrlForInstitutionalWebservices { get; }

        string UrlForMainInstitutionalWebsite { get; }
    }
}
