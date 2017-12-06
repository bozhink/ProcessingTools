namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors.Processors.Documents;
    using ProcessingTools.Contracts.Services.Data.Files;

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
