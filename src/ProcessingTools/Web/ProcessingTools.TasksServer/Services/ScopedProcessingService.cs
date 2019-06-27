// <copyright file="ScopedProcessingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.TasksServer.Services
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Scoped processing service.
    /// </summary>
    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedProcessingService"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void DoWork()
        {
            this.logger.LogInformation("Scoped Processing Service is working.");
        }
    }
}
