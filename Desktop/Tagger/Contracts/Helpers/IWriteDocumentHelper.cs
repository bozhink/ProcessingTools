namespace ProcessingTools.Tagger.Contracts.Helpers
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IWriteDocumentHelper
    {
        Task<object> Read(IDocument document, IProgramSettings settings);
    }
}
