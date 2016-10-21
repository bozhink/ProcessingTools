namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.Cloners;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.ZooBank.Models.Json;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    [Description("Clone ZooBank json.")]
    public class ZooBankCloneJsonController : TaggerControllerFactory, IZooBankCloneJsonController
    {
        private readonly IZoobankJsonCloner cloner;
        private readonly ILogger logger;

        public ZooBankCloneJsonController(IDocumentFactory documentFactory, IZoobankJsonCloner cloner, ILogger logger)
            : base(documentFactory)
        {
            if (cloner == null)
            {
                throw new ArgumentNullException(nameof(cloner));
            }

            this.cloner = cloner;
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
                throw new ApplicationException("The file path to json-file-to-clone should be set.");
            }

            string sourceFileName = settings.FileNames.ElementAt(2);
            var source = this.GetZoobankRegistrationObject(sourceFileName);

            await this.cloner.Clone(document, source);
        }

        private ZooBankRegistration GetZoobankRegistrationObject(string sourceFileName)
        {
            if (string.IsNullOrWhiteSpace(sourceFileName))
            {
                throw new ArgumentNullException(nameof(sourceFileName));
            }

            List<ZooBankRegistration> zoobankRegistrationList = null;
            using (var stream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ZooBankRegistration>));
                zoobankRegistrationList = (List<ZooBankRegistration>)serializer.ReadObject(stream);
                stream.Close();
            }

            ZooBankRegistration zoobankRegistration = null;

            if (zoobankRegistrationList == null || zoobankRegistrationList.Count < 1)
            {
                throw new ApplicationException("No valid ZooBank registration records in JSON file");
            }
            else
            {
                if (zoobankRegistrationList.Count > 1)
                {
                    this.logger?.Log(LogType.Warning, "More than one ZooBank registration records in JSON File.\n\tIt will be used only the first one.");
                }

                zoobankRegistration = zoobankRegistrationList[0];
            }

            return zoobankRegistration;
        }
    }
}
