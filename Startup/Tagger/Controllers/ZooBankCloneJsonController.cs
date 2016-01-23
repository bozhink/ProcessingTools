namespace ProcessingTools.MainProgram.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.ZooBank;
    using ProcessingTools.Contracts;

    public class ZooBankCloneJsonController : TaggerControllerFactory, IZooBankCloneJsonController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            string jsonStringContent = File.ReadAllText(settings.QueryFileName);
            var cloner = new ZoobankJsonCloner(jsonStringContent, document.OuterXml, logger);

            await cloner.Clone();

            document.LoadXml(cloner.Xml);
        }
    }
}