// <copyright file="ReadDocumentHelper.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.Layout;

    /// <summary>
    /// Read document helper.
    /// </summary>
    public class ReadDocumentHelper : IReadDocumentHelper
    {
        private readonly IDocumentMerger documentMerger;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentPostReadNormalizer documentNormalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadDocumentHelper"/> class.
        /// </summary>
        /// <param name="documentMerger">Document merger.</param>
        /// <param name="documentReader">Document reader.</param>
        /// <param name="documentNormalizer">Document normalizer.</param>
        public ReadDocumentHelper(IDocumentMerger documentMerger, IDocumentReader documentReader, IDocumentPostReadNormalizer documentNormalizer)
        {
            this.documentMerger = documentMerger ?? throw new ArgumentNullException(nameof(documentMerger));
            this.documentReader = documentReader ?? throw new ArgumentNullException(nameof(documentReader));
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
        }

        /// <inheritdoc/>
        public Task<IDocument> ReadAsync(bool mergeInputFiles, params string[] fileNames)
        {
            if (fileNames is null || fileNames.Length < 1)
            {
                throw new ArgumentNullException(nameof(fileNames));
            }

            return this.ReadInternalAsync(mergeInputFiles, fileNames);
        }

        private async Task<IDocument> ReadInternalAsync(bool mergeInputFiles, string[] fileNames)
        {
            IDocument document;

            if (mergeInputFiles)
            {
                document = await this.documentMerger.MergeAsync(fileNames).ConfigureAwait(false);
            }
            else
            {
                document = await this.documentReader.ReadDocumentAsync(fileNames[0]).ConfigureAwait(false);
            }

            await this.documentNormalizer.NormalizeAsync(document).ConfigureAwait(false);

            return document;
        }
    }
}
