namespace ProcessingTools.Processors.Contracts.Documents
{
    using ProcessingTools.Contracts;

    public interface IDocumentMerger
    {
        IDocument Merge(params string[] fileNames);
    }
}
