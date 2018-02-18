// <copyright file="DocumentWriter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Files;

    /// <summary>
    /// Document writer.
    /// </summary>
    public class DocumentWriter : IDocumentWriter
    {
        private readonly IXmlFileContentDataService filesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentWriter"/> class.
        /// </summary>
        /// <param name="filesManager">File manager.</param>
        public DocumentWriter(IXmlFileContentDataService filesManager)
        {
            this.filesManager = filesManager ?? throw new ArgumentNullException(nameof(filesManager));
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

            return this.filesManager.WriteXmlFile(fileName, document.XmlDocument);
        }
    }
}
