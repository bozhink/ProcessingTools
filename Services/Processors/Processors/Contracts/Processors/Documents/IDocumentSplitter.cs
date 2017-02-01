namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IDocumentSplitter
    {
        IEnumerable<IDocument> Split(IDocument document);
    }
}
