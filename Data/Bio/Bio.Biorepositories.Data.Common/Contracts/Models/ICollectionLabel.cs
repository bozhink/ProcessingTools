namespace ProcessingTools.Bio.Biorepositories.Data.Common.Contracts.Models
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
