namespace ProcessingTools.Bio.Biorepositories.Data.Common.Contracts.Models
{
    public interface ICollectionPer
    {
        string AccessEligibilityAndRules { get; }

        string CollectionCode { get; }

        string CollectionContentType { get; }

        string CollectionDescription { get; }

        string CollectionName { get; }

        string CoolUri { get; }

        string InstitutionName { get; }

        string Lsid { get; }

        string PreservationType { get; }

        string PrimaryContact { get; }

        string StatusOfCollection { get; }

        string Url { get; }

        string UrlForCollection { get; }

        string UrlForCollectionSpecimenCatalog { get; }

        string UrlForCollectionWebservices { get; }
    }
}
