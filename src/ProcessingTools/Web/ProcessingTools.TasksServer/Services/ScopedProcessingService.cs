// <copyright file="ScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.TasksServer.Services
{
    using System;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services;

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
        public void DoWork(Action<Exception> exceptionHandler)
        {
            this.logger.LogTrace($"Do work from {this.GetType().Name}");
        }

        /// <inheritdoc/>
        public void Start()
        {
            this.logger.LogTrace($"Starting {this.GetType().Name}...");
        }

        /// <inheritdoc/>
        public void Stop()
        {
            this.logger.LogTrace($"Stopping {this.GetType().Name}...");
        }
    }
}
