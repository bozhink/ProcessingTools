namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    public interface IDocumentXslProcessor : IDocumentProcessor
    {
        string XslFileFullName { get; set; }
    }
}
