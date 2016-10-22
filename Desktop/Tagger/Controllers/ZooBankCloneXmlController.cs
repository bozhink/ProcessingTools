namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.ZooBank;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    [Description("Clone ZooBank xml.")]
    public class ZooBankCloneXmlController : TaggerControllerFactory, IZooBankCloneXmlController
    {
        private readonly IZoobankXmlCloner cloner;

        public ZooBankCloneXmlController(IDocumentFactory documentFactory, IZoobankXmlCloner cloner)
            : base(documentFactory)
        {
            if (cloner == null)
            {
                throw new ArgumentNullException(nameof(cloner));
            }

            this.cloner = cloner;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
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

            await this.cloner.Clone(document, sourceDocument);
        }

        private IDocument ReadSourceDocument(string outputFileName, string sourceFileName)
        {
            var sourceDocument = this.DocumentFactory.Create();
            var fileProcessorNlm = new XmlFileProcessor(sourceFileName, outputFileName);
            fileProcessorNlm.Read(sourceDocument);
            return sourceDocument;
        }
    }
}
