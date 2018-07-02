// <copyright file="DocumentMerger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Documents;

    /// <summary>
    /// Document merger.
    /// </summary>
    public class DocumentMerger : IDocumentMerger
    {
        private readonly IDocumentReader documentReader;
        private readonly IDocumentWrapper documentWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentMerger"/> class.
        /// </summary>
        /// <param name="documentReader">Document reader.</param>
        /// <param name="documentWrapper">Document wrapper.</param>
        public DocumentMerger(IDocumentReader documentReader, IDocumentWrapper documentWrapper)
        {
            this.documentReader = documentReader ?? throw new ArgumentNullException(nameof(documentReader));
            this.documentWrapper = documentWrapper ?? throw new ArgumentNullException(nameof(documentWrapper));
        }

        /// <inheritdoc/>
        public async Task<IDocument> MergeAsync(params string[] fileNames)
        {
            if (fileNames == null || fileNames.Length < 1)
            {
                throw new ArgumentNullException(nameof(fileNames));
            }

            var cleanedFileNames = fileNames.Where(fn => !string.IsNullOrWhiteSpace(fn))
                .Distinct()
                .ToArray();

            if (cleanedFileNames.Length < 1)
            {
                throw new ArgumentException("No valid file names are provided", nameof(fileNames));
            }

            var document = this.documentWrapper.Create();

            foreach (var fileName in cleanedFileNames)
            {
                var readDocument = await this.documentReader.ReadDocumentAsync(fileName).ConfigureAwait(false);
                var fragment = document.XmlDocument.CreateDocumentFragment();
                fragment.InnerXml = readDocument.XmlDocument.DocumentElement.OuterXml;
                document.XmlDocument.DocumentElement.AppendChild(fragment);
            }

            document.SchemaType = SchemaType.System;

            return document;
        }
    }
}
