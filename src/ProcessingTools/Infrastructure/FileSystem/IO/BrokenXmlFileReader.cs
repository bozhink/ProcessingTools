namespace ProcessingTools.FileSystem.IO
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Files.IO;
    using ProcessingTools.Enumerations;

    public class BrokenXmlFileReader : IXmlFileReader
    {
        private readonly IXmlFileReader reader;
        private readonly ILogger logger;

        public BrokenXmlFileReader(IXmlFileReader reader, ILogger logger)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
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

        public async Task<XmlDocument> ReadXml(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            XmlDocument document;
            try
            {
                document = await this.reader.ReadXml(fullName).ConfigureAwait(false);
            }
            catch (XmlException e)
            {
                document = this.ProcessXmlException(fullName, e);
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count == 1 && e.InnerExceptions[0] is XmlException)
                {
                    document = this.ProcessXmlException(fullName, e.InnerExceptions[0]);
                }
                else
                {
                    throw;
                }
            }

            return document;
        }

        private XmlDocument ProcessXmlException(string fileName, Exception xmlException)
        {
            XmlDocument document;
            this.logger?.Log(
                LogType.Info,
                xmlException,
                "Input file name '{0}' is not a valid XML document.\nIt will be read as text file and will be wrapped in basic XML tags.\n",
                fileName);

            document = this.RestoreXmlDocument(fileName);
            return document;
        }

        private XmlDocument RestoreXmlDocument(object fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var body = document.CreateElement(ElementNames.Body);
            foreach (var line in File.ReadLines(fileName.ToString()))
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
