namespace ProcessingTools.FileSystem.IO
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Files.IO;
    using ProcessingTools.Contracts.Types;

    public class BrokenXmlFileReader : IXmlFileReader
    {
        private readonly IXmlFileReader reader;
        private readonly ILogger logger;

        public BrokenXmlFileReader(IXmlFileReader reader, ILogger logger)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            this.reader = reader;
            this.logger = logger;
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

        public XmlReader GetReader(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return this.reader.GetReader(fullName);
        }

        public Task<XmlDocument> ReadXml(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            XmlDocument document;
            try
            {
                document = this.reader.ReadXml(fullName).Result;
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count == 1 && e.InnerExceptions[0] is XmlException)
                {
                    this.logger?.Log(
                       LogType.Info,
                       e.InnerExceptions[0],
                       "Input file name '{0}' is not a valid XML document.\nIt will be read as text file and will be wrapped in basic XML tags.\n",
                       fullName);

                    document = this.RestoreXmlDocument(fullName);
                }
                else
                {
                    throw e;
                }
            }

            return Task.FromResult(document);
        }

        private XmlDocument RestoreXmlDocument(object fullName)
        {
            if (fullName == null)
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var body = document.CreateElement(ElementNames.Body);
            foreach (var line in File.ReadLines(fullName.ToString()))
            {
                var paragraph = document.CreateElement(ElementNames.Paragraph);
                paragraph.InnerText = line;
                body.AppendChild(paragraph);
            }

            var documentElement = document.CreateElement(ElementNames.Article);
            documentElement.AppendChild(body);

            document.AppendChild(documentElement);

            return document;
        }
    }
}
