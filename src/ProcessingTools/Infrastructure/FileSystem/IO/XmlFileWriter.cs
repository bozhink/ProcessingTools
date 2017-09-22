namespace ProcessingTools.FileSystem.IO
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.IO;

    public class XmlFileWriter : IXmlFileWriter
    {
        public XmlFileWriter()
        {
            this.WriterSettings = new XmlWriterSettings
            {
                Async = true,
                Encoding = Defaults.Encoding,
                Indent = false,
                IndentChars = " ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace,
                NewLineOnAttributes = false,
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                OmitXmlDeclaration = false,
                WriteEndDocumentOnClose = true,
                CloseOutput = true
            };
        }

        public XmlWriterSettings WriterSettings { get; set; }

        public Task<object> WriteAsync(string fileName, XmlDocument document)
        {
            return this.WriteAsync(fileName: fileName, document: document, documentType: null);
        }

        public async Task<object> WriteAsync(string fileName, XmlDocument document, XmlDocumentType documentType)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            using (XmlWriter writer = XmlWriter.Create(fileName, this.WriterSettings))
            {
                if (documentType != null)
                {
                    writer.WriteDocType(
                        documentType.Name,
                        documentType.PublicId,
                        documentType.SystemId,
                        documentType.InternalSubset);
                }

                document.DocumentElement.WriteTo(writer);
                await writer.FlushAsync().ConfigureAwait(false);
                writer.Close();
            }

            return true;
        }

        public Task<object> WriteAsync(string fileName, XmlReader reader)
        {
            return this.WriteAsync(fileName: fileName, reader: reader, documentType: null);
        }

        public async Task<object> WriteAsync(string fileName, XmlReader reader, XmlDocumentType documentType)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            using (XmlWriter writer = XmlWriter.Create(fileName, this.WriterSettings))
            {
                if (documentType != null)
                {
                    writer.WriteDocType(
                        documentType.Name,
                        documentType.PublicId,
                        documentType.SystemId,
                        documentType.InternalSubset);
                }

                await writer.WriteNodeAsync(reader, true).ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
                writer.Close();
            }

            return true;
        }
    }
}
