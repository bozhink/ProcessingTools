// <copyright file="BrokenXmlReadService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.IO
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// Broken XML read service.
    /// </summary>
    public class BrokenXmlReadService : IXmlReadService
    {
        private readonly IXmlReadService xmlReadService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrokenXmlReadService"/> class.
        /// </summary>
        /// <param name="xmlReadService">Instance of <see cref="IXmlReadService"/> to be wrapped.</param>
        public BrokenXmlReadService(IXmlReadService xmlReadService)
        {
            this.xmlReadService = xmlReadService ?? throw new ArgumentNullException(nameof(xmlReadService));
        }

        /// <inheritdoc/>
        public Encoding Encoding { get => this.xmlReadService.Encoding; set => this.xmlReadService.Encoding = value; }

        /// <inheritdoc/>
        public XmlReaderSettings ReaderSettings { get => this.xmlReadService.ReaderSettings; set => this.xmlReadService.ReaderSettings = value; }

        /// <inheritdoc/>
        public XmlReader GetXmlReaderForFile(string fileName) => this.xmlReadService.GetXmlReaderForFile(fileName);

        /// <inheritdoc/>
        public XmlReader GetXmlReaderForStream(Stream stream) => this.xmlReadService.GetXmlReaderForStream(stream);

        /// <inheritdoc/>
        public XmlReader GetXmlReaderForXmlString(string xml) => this.xmlReadService.GetXmlReaderForXmlString(xml);

        /// <inheritdoc/>
        public Stream GetStreamForXmlString(string xml) => this.xmlReadService.GetStreamForXmlString(xml);

        /// <inheritdoc/>
        public async Task<XmlDocument> ReadFileToXmlDocumentAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            XmlDocument document;
            try
            {
                document = await this.xmlReadService.ReadFileToXmlDocumentAsync(fileName).ConfigureAwait(false);
            }
            catch (XmlException)
            {
                document = this.RestoreXmlDocument(fileName);
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count == 1 && e.InnerExceptions[0] is XmlException)
                {
                    document = this.RestoreXmlDocument(fileName);
                }
                else
                {
                    throw;
                }
            }

            return document;
        }

        /// <inheritdoc/>
        public async Task<XmlDocument> ReadStreamToXmlDocumentAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            XmlDocument document;
            try
            {
                document = await this.xmlReadService.ReadStreamToXmlDocumentAsync(stream).ConfigureAwait(false);
            }
            catch (XmlException)
            {
                document = this.RestoreXmlDocument(stream);
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count == 1 && e.InnerExceptions[0] is XmlException)
                {
                    document = this.RestoreXmlDocument(stream);
                }
                else
                {
                    throw;
                }
            }

            return document;
        }

        /// <inheritdoc/>
        public Task<XmlDocument> ReadXmlReaderToXmlDocumentAsync(XmlReader reader) => this.xmlReadService.ReadXmlReaderToXmlDocumentAsync(reader);

        /// <inheritdoc/>
        public Task<XmlDocument> ReadXmlStringToXmlDocumentAsync(string xml) => this.xmlReadService.ReadXmlStringToXmlDocumentAsync(xml);

        private XmlDocument RestoreXmlDocument(string fileName)
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
            foreach (var line in File.ReadLines(fileName))
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

        private XmlDocument RestoreXmlDocument(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            stream.Position = 0;

            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var body = document.CreateElement(ElementNames.Body);

            using (StreamReader streamReader = new StreamReader(stream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var paragraph = document.CreateElement(ElementNames.Paragraph);
                    paragraph.InnerText = line;
                    body.AppendChild(paragraph);
                }
            }

            var documentElement = document.CreateElement(ElementNames.Article);
            documentElement.AppendChild(body);

            document.AppendChild(documentElement);

            return document;
        }
    }
}
