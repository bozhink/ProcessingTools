namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.ZooBank;
    using ProcessingTools.Contracts;

    [Description("Clone ZooBank json.")]
    public class ZooBankCloneJsonController : TaggerControllerFactory, IZooBankCloneJsonController
    {
        private readonly ILogger logger;

        public ZooBankCloneJsonController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            int numberOfFileNames = settings.FileNames.Count();

            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The file path to json-file-to-clone should be set.");
            }

            string jsonToCloneFileName = settings.FileNames.ElementAt(2);

            string jsonStringContent = File.ReadAllText(jsonToCloneFileName);
            var cloner = new ZoobankJsonCloner(jsonStringContent, document.Xml, this.logger);

            await cloner.Clone();

            document.Xml = cloner.Xml;
        }
    }
}