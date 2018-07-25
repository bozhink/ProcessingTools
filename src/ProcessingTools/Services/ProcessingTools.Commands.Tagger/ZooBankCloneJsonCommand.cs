// <copyright file="ZooBankCloneJsonCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    /// <summary>
    /// ZooBank clone JSON command.
    /// </summary>
    [System.ComponentModel.Description("Clone ZooBank JSON.")]
    public class ZooBankCloneJsonCommand : IZooBankCloneJsonCommand
    {
        private readonly IZooBankJsonCloner cloner;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZooBankCloneJsonCommand"/> class.
        /// </summary>
        /// <param name="cloner">Instance of <see cref="IZooBankJsonCloner"/>.</param>
        /// <param name="logger">Instance of <see cref="ILogger"/>.</param>
        public ZooBankCloneJsonCommand(IZooBankJsonCloner cloner, ILogger<ZooBankCloneJsonCommand> logger)
        {
            this.cloner = cloner ?? throw new ArgumentNullException(nameof(cloner));
            this.logger = logger;
        }

        /// <inheritdoc/>
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
                    this.logger.LogWarning("More than one ZooBank registration records in JSON File.\n\tIt will be used only the first one.");
                }

                zoobankRegistration = zoobankRegistrationList[0];
            }

            return zoobankRegistration;
        }
    }
}
