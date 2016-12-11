namespace ProcessingTools.Bio.Biorepositories.Data.Common.Contracts.Models
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
