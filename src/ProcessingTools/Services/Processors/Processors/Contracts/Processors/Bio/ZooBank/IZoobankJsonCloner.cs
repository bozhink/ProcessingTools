namespace ProcessingTools.Processors.Contracts.Processors.Bio.ZooBank
{
    using ProcessingTools.Bio.Taxonomy.ServiceClient.ZooBank.Models.Json;

    public interface IZoobankJsonCloner : IDocumentCloner<ZooBankRegistration>
    {
    }
}
