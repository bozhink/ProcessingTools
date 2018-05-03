namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    [System.ComponentModel.Description("Clone ZooBank JSON.")]
    public class ZooBankCloneJsonCommand : IZooBankCloneJsonCommand
    {
        private readonly IZooBankJsonCloner cloner;
        private readonly ILogger logger;

        public ZooBankCloneJsonCommand(IZooBankJsonCloner cloner, ILogger logger)
        {
            this.cloner = cloner ?? throw new ArgumentNullException(nameof(cloner));
            this.logger = logger;
        }

        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            int numberOfFileNames = settings.FileNames.Count;
            if (numberOfFileNames < 2)
            {
                throw new InvalidOperationException("Output file name should be set.");
            }

            if (numberOfFileNames < 3)
            {
                throw new InvalidOperationException("The file path to json-file-to-clone should be set.");
            }

            string sourceFileName = settings.FileNames[2];
            var source = this.GetZoobankRegistrationObject(sourceFileName);

            return await this.cloner.CloneAsync(document, source).ConfigureAwait(false);
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
                throw new ProcessingTools.Exceptions.InvalidDataException("No valid ZooBank registration records in JSON file");
            }
            else
            {
                if (zoobankRegistrationList.Count > 1)
                {
                    this.logger?.Log(type: LogType.Warning, message: "More than one ZooBank registration records in JSON File.\n\tIt will be used only the first one.");
                }

                zoobankRegistration = zoobankRegistrationList[0];
            }

            return zoobankRegistration;
        }
    }
}
