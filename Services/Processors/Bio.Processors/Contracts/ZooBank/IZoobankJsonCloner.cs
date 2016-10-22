namespace ProcessingTools.Bio.Processors.Contracts.ZooBank
{
    using ProcessingTools.Bio.Taxonomy.ServiceClient.ZooBank.Models.Json;
    using ProcessingTools.Contracts;

    public interface IZoobankJsonCloner : IGenericDocumentCloner<ZooBankRegistration>
    {
    }
}
