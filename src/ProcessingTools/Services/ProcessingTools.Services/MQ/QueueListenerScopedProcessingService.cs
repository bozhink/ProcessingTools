// <copyright file="QueueListenerScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.MQ
{
    using System;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services.MQ;

    /// <summary>
    /// Queue listener scoped processing service.
    /// </summary>
    public class QueueListenerScopedProcessingService : IQueueListenerScopedProcessingService
    {
        private readonly IQueueListener queueListener;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueListenerScopedProcessingService"/> class.
        /// </summary>
        /// <param name="queueListener">Queue listener.</param>
        /// <param name="logger">Logger.</param>
        public QueueListenerScopedProcessingService(IQueueListener queueListener, ILogger<QueueListenerScopedProcessingService> logger)
        {
            this.queueListener = queueListener ?? throw new ArgumentNullException(nameof(queueListener));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void Start()
        {
            this.logger.LogInformation("Queue Listener Scoped Processing Service is starting.");
            this.queueListener.Start();
        }

        /// <inheritdoc/>
        public void DoWork(Action<Exception> exceptionHandler)
        {
            try
            {
                this.logger.LogInformation("Queue Listener Scoped Processing Service is working.");
                this.queueListener.Run(exceptionHandler);
            }
            catch (Exception ex)
            {
                exceptionHandler.Invoke(ex);
            }
        }

        /// <inheritdoc/>
        public void Stop()
        {
            this.logger.LogInformation("Queue Listener Scoped Processing Service is stopping.");
            this.queueListener.Stop();
        }
    }
}
