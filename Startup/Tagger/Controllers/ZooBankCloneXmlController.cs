namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.ZooBank;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Clone ZooBank xml.")]
    public class ZooBankCloneXmlController : TaggerControllerFactory, IZooBankCloneXmlController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
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

            var cloner = new ZoobankXmlCloner(nlmDocument.Xml, document.OuterXml, logger);

            await cloner.Clone();

            document.LoadXml(cloner.Xml);
        }
    }
}