namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    public interface IDocumentXslProcessor : IDocumentProcessor
    {
        string XslFilePath { get; set; }
    }
}
