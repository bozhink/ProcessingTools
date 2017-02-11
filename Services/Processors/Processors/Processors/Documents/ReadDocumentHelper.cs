﻿namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Processors.Contracts.Processors.Documents;

    public class ReadDocumentHelper : IReadDocumentHelper
    {
        private readonly IDocumentFactory documentFactory;
        private readonly IDocumentMerger documentMerger;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentPostReadNormalizer documentNormalizer;

        public ReadDocumentHelper(
            IDocumentFactory documentFactory,
            IDocumentMerger documentMerger,
            IDocumentReader documentReader,
            IDocumentPostReadNormalizer documentNormalizer)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (documentMerger == null)
            {
                throw new ArgumentNullException(nameof(documentMerger));
            }

            if (documentReader == null)
            {
                throw new ArgumentNullException(nameof(documentReader));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.documentFactory = documentFactory;
            this.documentMerger = documentMerger;
            this.documentReader = documentReader;
            this.documentNormalizer = documentNormalizer;
        }

        public async Task<IDocument> Read(bool mergeInputFiles, params string[] fileNames)
        {
            if (fileNames == null || fileNames.Length < 1)
            {
                throw new ArgumentNullException(nameof(fileNames));
            }

            IDocument document;

            if (mergeInputFiles)
            {
                document = await this.documentMerger.Merge(fileNames);
            }
            else
            {
                document = await this.documentReader.ReadDocument(fileNames[0]);
            }

            await this.documentNormalizer.Normalize(document);

            return document;
        }
    }
}
