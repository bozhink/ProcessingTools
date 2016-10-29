namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    [Description("Clone ZooBank xml.")]
    public class ZooBankCloneXmlController : IZooBankCloneXmlController
    {
        private readonly IDocumentFactory documentFactory;
        private readonly IZoobankXmlCloner cloner;

        public ZooBankCloneXmlController(IDocumentFactory documentFactory, IZoobankXmlCloner cloner)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (cloner == null)
            {
                throw new ArgumentNullException(nameof(cloner));
            }

            this.documentFactory = documentFactory;
            this.cloner = cloner;
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

            int numberOfFileNames = settings.FileNames.Count();
            if (numberOfFileNames < 2)
            {
                throw new ApplicationException("Output file name should be set.");
            }

            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The file path to xml-file-to-clone should be set.");
            }

            string outputFileName = settings.FileNames.ElementAt(1);
            string sourceFileName = settings.FileNames.ElementAt(2);
            var sourceDocument = this.ReadSourceDocument(outputFileName, sourceFileName);

            return await this.cloner.Clone(document, sourceDocument);
        }

        private IDocument ReadSourceDocument(string outputFileName, string sourceFileName)
        {
            // TODO: DI
            var sourceDocument = this.documentFactory.Create();
            var fileProcessorNlm = new XmlFileProcessor(sourceFileName, outputFileName);
            fileProcessorNlm.Read(sourceDocument);
            return sourceDocument;
        }
    }
}
