namespace ProcessingTools.Processors.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Documents;

    public class DocumentManager : IDocumentManager
    {
        private readonly IReadDocumentHelper documentReader;
        private readonly IWriteDocumentHelper documentWriter;

        public DocumentManager(IReadDocumentHelper documentReader, IWriteDocumentHelper documentWriter)
        {
            if (documentReader == null)
            {
                throw new ArgumentNullException(nameof(documentReader));
            }

            if (documentWriter == null)
            {
                throw new ArgumentNullException(nameof(documentWriter));
            }

            this.documentReader = documentReader;
            this.documentWriter = documentWriter;
        }

        public async Task<IDocument> Read(bool mergeInputFiles, params string[] fileNames)
        {
            return await this.documentReader.Read(mergeInputFiles, fileNames);
        }

        public async Task<object> Write(string outputFileName, IDocument document, bool splitDocument)
        {
            return await this.documentWriter.Write(outputFileName, document, splitDocument);
        }
    }
}
