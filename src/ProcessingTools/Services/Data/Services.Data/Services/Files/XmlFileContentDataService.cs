namespace ProcessingTools.Services.Data.Services.Files
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.IO;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Services.Contracts.IO;

    public class XmlFileContentDataService : IXmlFileContentDataService
    {
        private readonly IXmlReadService reader;
        private readonly IXmlFileWriter writer;

        public XmlFileContentDataService(IXmlReadService reader, IXmlFileWriter writer)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public XmlReaderSettings ReaderSettings
        {
            get
            {
                return this.reader.ReaderSettings;
            }

            set
            {
                this.reader.ReaderSettings = value;
            }
        }

        public XmlWriterSettings WriterSettings
        {
            get
            {
                return this.writer.WriterSettings;
            }

            set
            {
                this.writer.WriterSettings = value;
            }
        }

        public Task<XmlDocument> ReadXmlFile(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return this.reader.ReadFileToXmlDocumentAsync(fileName: fullName);
        }

        public async Task<object> WriteXmlFile(string fullName, XmlDocument document, XmlDocumentType documentType = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return await this.writer.WriteAsync(fileName: fullName, document: document, documentType: documentType).ConfigureAwait(false);
        }
    }
}
