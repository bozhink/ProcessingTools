namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Processors.Contracts.Processors.Documents;

    public class WriteDocumentHelper : IWriteDocumentHelper
    {
        private readonly IDocumentSplitter documentSplitter;
        private readonly IDocumentWriter documentWriter;
        private readonly IDocumentPreWriteNormalizer documentNormalizer;

        public WriteDocumentHelper(
            IDocumentSplitter documentSplitter,
            IDocumentWriter documentWriter,
            IDocumentPreWriteNormalizer documentNormalizer)
        {
            if (documentSplitter == null)
            {
                throw new ArgumentNullException(nameof(documentSplitter));
            }

            if (documentWriter == null)
            {
                throw new ArgumentNullException(nameof(documentWriter));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.documentSplitter = documentSplitter;
            this.documentWriter = documentWriter;
            this.documentNormalizer = documentNormalizer;
        }

        public async Task<object> Write(string outputFileName, IDocument document, bool splitDocument)
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

                    await this.WriteSingleDocument(path, subdocument);
                }
            }
            else
            {
                await this.WriteSingleDocument(outputFileName, document);
            }

            return true;
        }

        private async Task<object> WriteSingleDocument(string fileName, IDocument document)
        {
            if (document == null)
            {
                return false;
            }

            // Due to some XSL characteristics, double normalization is better than a single one.
            var result = await this.documentNormalizer.Normalize(document)
                .ContinueWith(
                    _ =>
                    {
                        _.Wait();
                        return this.documentWriter.WriteDocument(fileName, document).Result;
                    });

            return result;
        }
    }
}
