namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts
{
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Models;
    using ProcessingTools.Contracts;

    public interface IGbifApiV09DataRequester : IDataRequester<GbifApiV09ResponseModel>
    {
    }
}
