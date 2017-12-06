namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors.Processors.Documents;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    public class ReadDocumentHelper : IReadDocumentHelper
    {
        private readonly IDocumentMerger documentMerger;
        private readonly IDocumentReader documentReader;
        private readonly IDocumentPostReadNormalizer documentNormalizer;

        public ReadDocumentHelper(
            IDocumentMerger documentMerger,
            IDocumentReader documentReader,
            IDocumentPostReadNormalizer documentNormalizer)
        {
            this.documentMerger = documentMerger ?? throw new ArgumentNullException(nameof(documentMerger));
            this.documentReader = documentReader ?? throw new ArgumentNullException(nameof(documentReader));
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
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

            await this.documentNormalizer.NormalizeAsync(document);

            return document;
        }
    }
}
