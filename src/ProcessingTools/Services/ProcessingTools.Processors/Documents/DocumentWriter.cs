// <copyright file="DocumentWriter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// Document writer.
    /// </summary>
    public class DocumentWriter : IDocumentWriter
    {
        private readonly IXmlWriteService writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentWriter"/> class.
        /// </summary>
        /// <param name="writer">XML writer..</param>
        public DocumentWriter(IXmlWriteService writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public Task<object> WriteDocumentAsync(string fileName, IDocument document)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return this.writer.WriteAsync(fileName, document.XmlDocument);
        }
    }
}
