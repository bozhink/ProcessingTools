namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Documents;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Data.Contracts.Files;

    public class DocumentReader : IDocumentReader
    {
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlFileContentDataService filesManager;

        public DocumentReader(IDocumentFactory documentFactory, IXmlFileContentDataService filesManager)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (filesManager == null)
            {
                throw new ArgumentNullException(nameof(filesManager));
            }

            this.documentFactory = documentFactory;
            this.filesManager = filesManager;
        }

        public async Task<IDocument> ReadDocument(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var xmldocument = await this.filesManager.ReadXmlFile(fileName);

            var document = this.documentFactory.Create(xmldocument.OuterXml);
            switch (document.XmlDocument.DocumentElement.Name)
            {
                case ElementNames.Article:
                    document.SchemaType = SchemaType.Nlm;
                    break;

                default:
                    document.SchemaType = SchemaType.System;
                    break;
            }

            return document;
        }
    }
}
