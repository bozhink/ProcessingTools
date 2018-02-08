namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors.Processors.Documents;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Contracts.Files;

    public class DocumentReader : IDocumentReader
    {
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlFileContentDataService filesManager;

        public DocumentReader(IDocumentFactory documentFactory, IXmlFileContentDataService filesManager)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.filesManager = filesManager ?? throw new ArgumentNullException(nameof(filesManager));
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
