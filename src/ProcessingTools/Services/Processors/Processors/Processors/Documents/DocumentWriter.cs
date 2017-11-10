namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Documents;
    using ProcessingTools.Services.Contracts.Data.Files;

    public class DocumentWriter : IDocumentWriter
    {
        private readonly IXmlFileContentDataService filesManager;

        public DocumentWriter(IXmlFileContentDataService filesManager)
        {
            this.filesManager = filesManager ?? throw new ArgumentNullException(nameof(filesManager));
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
