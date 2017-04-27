namespace ProcessingTools.Processors.Contracts.Processors
{
    using ProcessingTools.Contracts;

    public interface IDocumentXslProcessor : IDocumentProcessor
    {
        string XslFileFullName { get; set; }
    }
}
