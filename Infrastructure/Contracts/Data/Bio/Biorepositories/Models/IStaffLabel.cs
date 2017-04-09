namespace ProcessingTools.Contracts.Data.Bio.Biorepositories.Models
{
    public interface IStaffLabel
    {
        string CityTown { get; }

        string Country { get; }

        string PostalZipCode { get; }

        string PrimaryInstitution { get; }

        string StaffMemberFullName { get; }

        string StateProvince { get; }
    }
}
