namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IDocumentSplitter
    {
        Task<object> Split(IDocument document);
    }
}
