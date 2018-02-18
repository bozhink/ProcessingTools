// <copyright file="WriteDocumentHelper.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Processors.Contracts.Layout;

    /// <summary>
    /// Write document helper.
    /// </summary>
    public class WriteDocumentHelper : IWriteDocumentHelper
    {
        private readonly IDocumentSplitter documentSplitter;
        private readonly IDocumentWriter documentWriter;
        private readonly IDocumentPreWriteNormalizer documentNormalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteDocumentHelper"/> class.
        /// </summary>
        /// <param name="documentSplitter">Document splitter.</param>
        /// <param name="documentWriter">Document writer.</param>
        /// <param name="documentNormalizer">Document normalizer.</param>
        public WriteDocumentHelper(IDocumentSplitter documentSplitter, IDocumentWriter documentWriter, IDocumentPreWriteNormalizer documentNormalizer)
        {
            this.documentSplitter = documentSplitter ?? throw new ArgumentNullException(nameof(documentSplitter));
            this.documentWriter = documentWriter ?? throw new ArgumentNullException(nameof(documentWriter));
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
        }

        /// <inheritdoc/>
        public async Task<object> WriteAsync(string outputFileName, IDocument document, bool splitDocument)
        {
            if (string.IsNullOrWhiteSpace(outputFileName))
            {
                throw new ArgumentNullException(nameof(outputFileName));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (splitDocument)
            {
                var subdocuments = this.documentSplitter.Split(document);
                foreach (var subdocument in subdocuments)
                {
                    var fileName = subdocument.GenerateFileNameFromDocumentId();

                    var path = Path.Combine(
                        Path.GetDirectoryName(outputFileName),
                        $"{fileName}.{FileConstants.XmlFileExtension}");

                    await this.WriteSingleDocumentAsync(path, subdocument).ConfigureAwait(false);
                }
            }
            else
            {
                await this.WriteSingleDocumentAsync(outputFileName, document).ConfigureAwait(false);
            }

            return true;
        }

        private async Task<object> WriteSingleDocumentAsync(string fileName, IDocument document)
        {
            if (document == null)
            {
                return false;
            }

            // Due to some XSL characteristics, double normalization is better than a single one.
            var result = await this.documentNormalizer.NormalizeAsync(document)
                .ContinueWith(
                    _ =>
                    {
                        _.Wait();
                        return this.documentWriter.WriteDocumentAsync(fileName, document).Result;
                    })
                .ConfigureAwait(false);

            return result;
        }
    }
}
