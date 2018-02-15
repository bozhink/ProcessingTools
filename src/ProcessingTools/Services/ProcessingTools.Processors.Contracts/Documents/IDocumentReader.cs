namespace ProcessingTools.Contracts.Processors.Processors.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IDocumentReader
    {
        Task<IDocument> ReadDocument(string fileName);
    }
}
