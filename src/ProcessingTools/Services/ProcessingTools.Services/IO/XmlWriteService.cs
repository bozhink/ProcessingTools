// <copyright file="XmlWriteService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.IO
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// XML write service.
    /// </summary>
    public class XmlWriteService : IXmlWriteService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriteService"/> class.
        /// </summary>
        public XmlWriteService()
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

        /// <inheritdoc/>
        public XmlWriterSettings WriterSettings { get; set; }

        /// <inheritdoc/>
        public Task<object> WriteAsync(string fileName, XmlDocument document)
        {
            return this.WriteAsync(fileName: fileName, document: document, documentType: null);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<object> WriteAsync(string fileName, XmlReader reader)
        {
            return this.WriteAsync(fileName: fileName, reader: reader, documentType: null);
        }

        /// <inheritdoc/>
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
