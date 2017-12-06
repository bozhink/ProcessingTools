namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors.Processors.Documents;

    public class DocumentSplitter : IDocumentSplitter
    {
        private readonly IDocumentFactory documentFactory;

        public DocumentSplitter(IDocumentFactory documentFactory)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        public IEnumerable<IDocument> Split(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return document.SelectNodes(XPathStrings.HigherDocumentStructure)
                .Select(n => this.documentFactory.Create(n.OuterXml));
        }
    }
}
