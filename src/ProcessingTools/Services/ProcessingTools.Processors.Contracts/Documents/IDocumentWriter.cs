namespace ProcessingTools.Contracts.Processors.Processors.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IDocumentWriter
    {
        Task<object> WriteDocument(string fileName, IDocument document);
    }
}
