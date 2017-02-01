namespace ProcessingTools.Tagger.Core.Helpers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Helpers;
    using Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Processors.Contracts.Processors.Documents;

    public class WriteDocumentHelper : IWriteDocumentHelper
    {
        private readonly IDocumentSplitter documentSplitter;
        private readonly IDocumentWriter documentWriter;
        private readonly IDocumentNormalizer documentNormalizer;

        public WriteDocumentHelper(
            IDocumentSplitter documentSplitter,
            IDocumentWriter documentWriter,
            IDocumentNormalizer documentNormalizer)
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

        public async Task<object> Write(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (settings.SplitDocument)
            {
                var subdocuments = this.documentSplitter.Split(document);
                foreach (var subdocument in subdocuments)
                {
                    var fileName = subdocument.GenerateFileNameFromDocumentId();

                    var path = Path.Combine(
                        Path.GetDirectoryName(settings.OutputFileName),
                        $"{fileName}.{FileConstants.XmlFileExtension}");

                    await this.WriteSingleDocument(path, subdocument);
                }
            }
            else
            {
                await this.WriteSingleDocument(settings.OutputFileName, document);
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
            var result = await this.documentNormalizer.NormalizeToDocumentSchema(document)
                .ContinueWith(
                    _ =>
                    {
                        _.Wait();
                        return this.documentNormalizer.NormalizeToDocumentSchema(document);
                    })
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
