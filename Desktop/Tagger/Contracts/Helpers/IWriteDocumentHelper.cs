namespace ProcessingTools.Tagger.Contracts.Helpers
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IWriteDocumentHelper
    {
        Task<object> Write(string outputFileName, IDocument document, bool splitDocument);
    }
}
