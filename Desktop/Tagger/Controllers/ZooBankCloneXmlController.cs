namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.ZooBank;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    [Description("Clone ZooBank xml.")]
    public class ZooBankCloneXmlController : TaggerControllerFactory, IZooBankCloneXmlController
    {
        private readonly ILogger logger;

        public ZooBankCloneXmlController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
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
            string xmlToCloneFileName = settings.FileNames.ElementAt(2);

            var nlmDocument = new TaxPubDocument();
            var fileProcessorNlm = new XmlFileProcessor(xmlToCloneFileName, outputFileName);
            fileProcessorNlm.Read(nlmDocument);

            var cloner = new ZoobankXmlCloner(nlmDocument.Xml, document.Xml, this.logger);

            await cloner.Clone();

            document.Xml = cloner.Xml;
        }
    }
}