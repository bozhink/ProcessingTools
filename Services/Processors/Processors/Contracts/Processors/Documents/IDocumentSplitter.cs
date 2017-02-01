namespace ProcessingTools.Processors.Contracts.Processors.Documents
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IDocumentSplitter
    {
        IEnumerable<IDocument> Split(IDocument document);
    }
}
