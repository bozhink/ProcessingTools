namespace ProcessingTools.Services.Data.Files
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Files;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Files.Generators;
    using ProcessingTools.Contracts.Files.IO;

    public class XmlFileContentDataService : IXmlFileContentDataService
    {
        private readonly IXmlFileReader reader;
        private readonly IXmlFileWriter writer;
        private readonly IFileNameGenerator fileNameGenerator;

        public XmlFileContentDataService(
            IXmlFileReader reader,
            IXmlFileWriter writer,
            IFileNameGenerator fileNameGenerator)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (fileNameGenerator == null)
            {
                throw new ArgumentNullException(nameof(fileNameGenerator));
            }

            this.reader = reader;
            this.writer = writer;
            this.fileNameGenerator = fileNameGenerator;
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

        public async Task<object> WriteXmlFile(
            string fullName,
            XmlDocument document,
            XmlDocumentType documentType = null,
            bool writeWithNewFileName = false)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string outputFileFullName = fullName;
            if (writeWithNewFileName)
            {
                outputFileFullName = await this.fileNameGenerator.Generate(
                    fullName,
                    FileConstants.MaximalLengthOfGeneratedNewFileName,
                    true);
            }

            await this.writer.Write(fullName: outputFileFullName, document: document, documentType: documentType);

            return outputFileFullName;
        }
    }
}