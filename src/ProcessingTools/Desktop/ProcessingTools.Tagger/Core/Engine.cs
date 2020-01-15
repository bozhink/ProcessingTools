// <copyright file="Engine.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Tagger.Contracts;

    /// <summary>
    /// Tagger's engine.
    /// </summary>
    public class Engine : IEngine
    {
        private readonly IFileProcessor fileProcessor;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="fileProcessor">Instance of <see cref="IFileProcessor"/>.</param>
        /// <param name="logger">Logger.</param>
        public Engine(IFileProcessor fileProcessor, ILogger<Engine> logger)
        {
            this.fileProcessor = fileProcessor ?? throw new ArgumentNullException(nameof(fileProcessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Run engine")]
        public async Task RunAsync(string[] args)
        {
            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(this.logger, args);
                var settings = settingsBuilder.Settings;

                await this.fileProcessor.RunAsync(settings).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, string.Empty);
            }
        }
    }
}
