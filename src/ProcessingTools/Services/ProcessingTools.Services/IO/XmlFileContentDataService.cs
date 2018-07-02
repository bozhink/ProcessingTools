// <copyright file="XmlFileContentDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.IO
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// XML file content data service.
    /// </summary>
    public class XmlFileContentDataService : IXmlFileContentDataService
    {
        private readonly IXmlReadService reader;
        private readonly IXmlWriteService writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileContentDataService"/> class.
        /// </summary>
        /// <param name="reader">XML reader.</param>
        /// <param name="writer">XML writer.</param>
        public XmlFileContentDataService(IXmlReadService reader, IXmlWriteService writer)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public XmlReaderSettings ReaderSettings { get => this.reader.ReaderSettings; set => this.reader.ReaderSettings = value; }

        /// <inheritdoc/>
        public XmlWriterSettings WriterSettings { get => this.writer.WriterSettings; set => this.writer.WriterSettings = value; }

        /// <inheritdoc/>
        public Task<XmlDocument> ReadXmlFile(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return this.reader.ReadFileToXmlDocumentAsync(fileName: fullName);
        }

        /// <inheritdoc/>
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
