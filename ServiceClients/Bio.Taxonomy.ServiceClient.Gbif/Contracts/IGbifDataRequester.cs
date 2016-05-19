namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts
{
    using Models;
    using ProcessingTools.Contracts;

    public interface IGbifDataRequester : IDataRequester<GbifApiResponseModel>
    {
    }
}
