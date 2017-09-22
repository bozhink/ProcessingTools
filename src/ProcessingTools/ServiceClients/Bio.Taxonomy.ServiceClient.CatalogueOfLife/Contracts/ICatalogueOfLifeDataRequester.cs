namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts
{
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models;
    using ProcessingTools.Processors.Contracts;

    public interface ICatalogueOfLifeDataRequester : IDataRequester<CatalogueOfLifeApiServiceResponse>
    {
    }
}
