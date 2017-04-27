namespace ProcessingTools.Processors.Contracts.Processors
{
    using ProcessingTools.Contracts;

    public interface IDocumentXQueryProcessor : IDocumentProcessor
    {
        string XQueryFileFullName { get; set; }
    }
}
