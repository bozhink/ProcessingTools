namespace ProcessingTools.Clients.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models;
    using ProcessingTools.Contracts;

    public interface ICatalogueOfLifeDataRequester : IDataRequester<CatalogueOfLifeApiServiceResponse>
    {
    }
}
