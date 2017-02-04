namespace ProcessingTools.Tagger.Contracts.Helpers
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Tagger.Commands.Contracts;

    public interface IWriteDocumentHelper
    {
        Task<object> Write(IDocument document, IProgramSettings settings);
    }
}
