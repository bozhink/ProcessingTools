namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IDocumentWriter
    {
        Task<object> WriteDocument(string fileName, IDocument document);
    }
}
