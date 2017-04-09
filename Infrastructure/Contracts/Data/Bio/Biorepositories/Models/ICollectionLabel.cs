namespace ProcessingTools.Contracts.Data.Bio.Biorepositories.Models
{
    public interface ICollectionLabel
    {
        string CityTown { get; }

        string CollectionName { get; }

        string Country { get; }

        string PostalZipCode { get; }

        string PrimaryContact { get; }

        string StateProvince { get; }
    }
}
