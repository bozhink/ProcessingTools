namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.ZooBank;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    public class ZooBankCloneXmlController : TaggerControllerFactory, IZooBankCloneXmlController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var nlmDocument = new TaxPubDocument();
            var fileProcessorNlm = new XmlFileProcessor(settings.QueryFileName, settings.OutputFileName);
            fileProcessorNlm.Read(nlmDocument);

            var cloner = new ZoobankXmlCloner(nlmDocument.Xml, document.OuterXml, logger);

            await cloner.Clone();

            document.LoadXml(cloner.Xml);
        }
    }
}