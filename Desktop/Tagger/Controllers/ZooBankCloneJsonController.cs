namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;

    using Contracts.Controllers;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.ZooBank.Models.Json;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    [Description("Clone ZooBank JSON.")]
    public class ZooBankCloneJsonController : IZooBankCloneJsonController
    {
        private readonly IZoobankJsonCloner cloner;
        private readonly ILogger logger;

        public ZooBankCloneJsonController(IZoobankJsonCloner cloner, ILogger logger)
        {
            if (cloner == null)
            {
                throw new ArgumentNullException(nameof(cloner));
            }

            this.cloner = cloner;
            this.logger = logger;
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
                throw new ApplicationException("The file path to json-file-to-clone should be set.");
            }

            string sourceFileName = settings.FileNames.ElementAt(2);
            var source = this.GetZoobankRegistrationObject(sourceFileName);

            return await this.cloner.Clone(document, source);
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
