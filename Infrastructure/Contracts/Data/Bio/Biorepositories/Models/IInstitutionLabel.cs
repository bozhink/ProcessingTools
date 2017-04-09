namespace ProcessingTools.Contracts.Data.Bio.Biorepositories.Models
{
    public interface IInstitutionLabel
    {
        string CityTown { get; }

        string Country { get; }

        string NameOfInstitution { get; }

        string PostalZipCode { get; }

        string PrimaryContact { get; }

        string StateProvince { get; }
    }
}
