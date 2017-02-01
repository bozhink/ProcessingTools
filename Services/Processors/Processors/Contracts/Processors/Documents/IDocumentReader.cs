namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IDocumentReader
    {
        Task<IDocument> ReadDocument(string fileName);
    }
}
