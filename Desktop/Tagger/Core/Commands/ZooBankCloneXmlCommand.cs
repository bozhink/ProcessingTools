namespace ProcessingTools.Tagger.Core.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Files.IO;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    [Description("Clone ZooBank xml.")]
    public class ZooBankCloneXmlCommand : IZooBankCloneXmlCommand
    {
        private readonly IZoobankXmlCloner cloner;
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlFileReader fileReader;

        public ZooBankCloneXmlCommand(IDocumentFactory documentFactory, IZoobankXmlCloner cloner, IXmlFileReader fileReader)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (cloner == null)
            {
                throw new ArgumentNullException(nameof(cloner));
            }

            if (fileReader == null)
            {
                throw new ArgumentNullException(nameof(fileReader));
            }

            this.documentFactory = documentFactory;
            this.cloner = cloner;
            this.fileReader = fileReader;
        }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
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
                throw new ApplicationException("Output file name should be set.");
            }

            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The file path to xml-file-to-clone should be set.");
            }

            string sourceFileName = settings.FileNames[2];
            var sourceDocument = await this.ReadSourceDocument(sourceFileName);

            return await this.cloner.Clone(document, sourceDocument);
        }

        private async Task<IDocument> ReadSourceDocument(string sourceFileName)
        {
            var xml = await this.fileReader.ReadXml(sourceFileName);
            var document = this.documentFactory.Create(xml.OuterXml);
            return document;
        }
    }
}
