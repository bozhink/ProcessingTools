namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.ZooBank;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Clone ZooBank json.")]
    public class ZooBankCloneJsonController : TaggerControllerFactory, IZooBankCloneJsonController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            int numberOfFileNames = settings.FileNames.Count();

            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The file path to json-file-to-clone should be set.");
            }

            string jsonToCloneFileName = settings.FileNames.ElementAt(2);

            string jsonStringContent = File.ReadAllText(jsonToCloneFileName);
            var cloner = new ZoobankJsonCloner(jsonStringContent, document.OuterXml, logger);

            await cloner.Clone();

            document.LoadXml(cloner.Xml);
        }
    }
}