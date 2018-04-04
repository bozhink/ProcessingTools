﻿namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;
    using ProcessingTools.Services.Contracts.IO;

    [System.ComponentModel.Description("Clone ZooBank XML.")]
    public class ZooBankCloneXmlCommand : IZooBankCloneXmlCommand
    {
        private readonly IZooBankXmlCloner cloner;
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlReadService reader;

        public ZooBankCloneXmlCommand(IDocumentFactory documentFactory, IZooBankXmlCloner cloner, IXmlReadService reader)
        {
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.cloner = cloner ?? throw new ArgumentNullException(nameof(cloner));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            int numberOfFileNames = settings.FileNames.Count;
            if (numberOfFileNames < 2)
            {
                throw new InvalidOperationException("Output file name should be set.");
            }

            if (numberOfFileNames < 3)
            {
                throw new InvalidOperationException("The file path to xml-file-to-clone should be set.");
            }

            string sourceFileName = settings.FileNames[2];
            var sourceDocument = await this.ReadSourceDocument(sourceFileName).ConfigureAwait(false);

            return await this.cloner.CloneAsync(document, sourceDocument).ConfigureAwait(false);
        }

        private async Task<IDocument> ReadSourceDocument(string sourceFileName)
        {
            var xml = await this.reader.ReadFileToXmlDocumentAsync(sourceFileName).ConfigureAwait(false);
            var document = this.documentFactory.Create(xml.OuterXml);
            return document;
        }
    }
}
