using System;
using System.Threading.Tasks;
using System.Xml;
using ProcessingTools.Contracts.Files.IO;
using ProcessingTools.Services.Data.Contracts.Files;

namespace ProcessingTools.Services.Data.Services.Files
{
    public class XmlFileContentDataService : IXmlFileContentDataService
    {
        private readonly IXmlFileReader reader;
        private readonly IXmlFileWriter writer;

        public XmlFileContentDataService(IXmlFileReader reader, IXmlFileWriter writer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            this.reader = reader;
            this.writer = writer;
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

            return this.reader.ReadXml(fullName: fullName);
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

            return await this.writer.Write(fullName: fullName, document: document, documentType: documentType);
        }
    }
}