// <copyright file="DocumentManager.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Documents;

    /// <summary>
    /// Document manager.
    /// </summary>
    public class DocumentManager : IDocumentManager
    {
        private readonly IReadDocumentHelper documentReader;
        private readonly IWriteDocumentHelper documentWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentManager"/> class.
        /// </summary>
        /// <param name="documentReader">Document reader.</param>
        /// <param name="documentWriter">Document writer.</param>
        public DocumentManager(IReadDocumentHelper documentReader, IWriteDocumentHelper documentWriter)
        {
            this.documentReader = documentReader ?? throw new ArgumentNullException(nameof(documentReader));
            this.documentWriter = documentWriter ?? throw new ArgumentNullException(nameof(documentWriter));
        }

        /// <inheritdoc/>
        public async Task<IDocument> ReadAsync(bool mergeInputFiles, params string[] fileNames)
        {
            return await this.documentReader.ReadAsync(mergeInputFiles, fileNames).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> WriteAsync(string outputFileName, IDocument document, bool splitDocument)
        {
            return await this.documentWriter.WriteAsync(outputFileName, document, splitDocument).ConfigureAwait(false);
        }
    }
}
