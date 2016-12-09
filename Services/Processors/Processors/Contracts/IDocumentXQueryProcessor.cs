namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    public interface IDocumentXQueryProcessor : IDocumentProcessor
    {
        string XQueryFileFullName { get; set; }
    }
}
