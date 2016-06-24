namespace ProcessingTools.FileSystem
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using ProcessingTools.Common;

    public class XmlFileReaderWriter : IXmlFileReaderWriter
    {
        private XmlReaderSettings readerSettings;
        private XmlWriterSettings writerSettings;

        public XmlFileReaderWriter()
        {
            this.ReaderSettings = new XmlReaderSettings
            {
                Async = true,
                CloseInput = true,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = false,
                IgnoreProcessingInstructions = false,
                IgnoreWhitespace = false
            };

            this.WriterSettings = new XmlWriterSettings
            {
                Async = true,
                CloseOutput = true,
                Encoding = Defaults.DefaultEncoding,
                Indent = false,
                IndentChars = " ",
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                NewLineChars = "\n",
                NewLineOnAttributes = false,
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false,
                WriteEndDocumentOnClose = true
            };
        }

        public XmlReaderSettings ReaderSettings
        {
            get
            {
                return this.readerSettings;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(this.ReaderSettings));
                }

                this.readerSettings = value;
            }
        }

        public XmlWriterSettings WriterSettings
        {
            get
            {
                return this.writerSettings;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(this.WriterSettings));
                }

                this.writerSettings = value;
            }
        }

        public StreamReader GetReader(string fileName, string basePath = null)
        {
            string path = this.CombineFileName(fileName, basePath);
            var reader = new StreamReader(path);
            return reader;
        }

        public XmlReader GetXmlReader(string fileName, string basePath = null)
        {
            string path = this.CombineFileName(fileName, basePath);
            var reader = XmlReader.Create(fileName, this.ReaderSettings);
            return reader;
        }

        public Stream ReadToStream(string fileName, string basePath = null)
        {
            string path = this.CombineFileName(fileName, basePath);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return stream;
        }

        public async Task Write(Stream stream, string fileName, string basePath = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            string path = this.CombineFileName(fileName, basePath);
            using (var writer = XmlWriter.Create(path, this.WriterSettings))
            {
                using (var reader = XmlReader.Create(stream, this.ReaderSettings))
                {
                    await writer.WriteNodeAsync(reader, true);
                    reader.Close();
                }

                await writer.FlushAsync();
                writer.Close();
            }
        }

        private string CombineFileName(string fileName, string basePath)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            Regex matchDriveLetter = new Regex(@"\A\W+:/");

            string path = null;
            if (string.IsNullOrWhiteSpace(basePath))
            {
                path = fileName;
            }
            else
            {
                path = Path.Combine(basePath, matchDriveLetter.Replace(fileName, string.Empty));
            }

            return path;
        }
    }
}
