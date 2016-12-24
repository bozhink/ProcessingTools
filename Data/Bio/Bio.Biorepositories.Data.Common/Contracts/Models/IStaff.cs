namespace ProcessingTools.Bio.Biorepositories.Data.Common.Contracts.Models
{
    public interface IStaff
    {
        string AdditionalAffiliations { get; }

        string AreaOfResponsibility { get; }

        string BirthYear { get; }

        string CityTown { get; }

        string Country { get; }

        string Email { get; }

        string FaxNumber { get; }

        string FirstName { get; }

        string JobTitle { get; }

        string LastName { get; }

        string PhoneNumber { get; }

        string PostalZipCode { get; }

        string PrimaryCollection { get; }

        string PrimaryInstitution { get; }

        string ResearchDiscipline { get; }

        string ResearchSpecialty { get; }

        string StaffMemberFullName { get; }

        string StateProvince { get; }

        string Url { get; }
    }
}
