namespace ProcessingTools.Tagger.Contracts.Helpers
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IReadDocumentHelper
    {
        Task<IDocument> Read(IProgramSettings settings);
    }
}
