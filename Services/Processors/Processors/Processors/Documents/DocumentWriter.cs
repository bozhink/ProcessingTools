namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Documents;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Data.Contracts.Files;

    public class DocumentWriter : IDocumentWriter
    {
        private readonly IXmlFileContentDataService filesManager;

        public DocumentWriter(IXmlFileContentDataService filesManager)
        {
            if (filesManager == null)
            {
                throw new ArgumentNullException(nameof(filesManager));
            }

            this.filesManager = filesManager;
        }

        public Task<object> WriteDocument(string fileName, IDocument document)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return this.filesManager.WriteXmlFile(fileName, document.XmlDocument);
        }
    }
}
