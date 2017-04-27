namespace ProcessingTools.Processors.Contracts.Processors.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IDocumentMerger
    {
        Task<IDocument> Merge(params string[] fileNames);
    }
}
