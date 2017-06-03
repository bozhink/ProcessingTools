namespace ProcessingTools.FileSystem.IO
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Files.IO;

    public class XmlFileWriter : IXmlFileWriter
    {
        public XmlFileWriter()
        {
            this.WriterSettings = new XmlWriterSettings()
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

        public async Task<object> Write(string fullName, XmlDocument document, XmlDocumentType documentType = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            using (XmlWriter writer = XmlWriter.Create(fullName, this.WriterSettings))
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
                await writer.FlushAsync();
                writer.Close();
            }

            return true;
        }

        public async Task<object> Write(string fullName, XmlReader reader, XmlDocumentType documentType = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            using (XmlWriter writer = XmlWriter.Create(fullName, this.WriterSettings))
            {
                if (documentType != null)
                {
                    writer.WriteDocType(
                        documentType.Name,
                        documentType.PublicId,
                        documentType.SystemId,
                        documentType.InternalSubset);
                }

                await writer.WriteNodeAsync(reader, true);
                await writer.FlushAsync();
                writer.Close();
            }

            return true;
        }
    }
}
