// <copyright file="QueueListenerScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.MQ
{
    using System;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Resources;
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
            this.logger.LogInformation(StringResources.QueueListenerScopedProcessingServiceStarting);
            this.queueListener.Start();
        }

        /// <inheritdoc/>
        public void DoWork(Action<Exception> exceptionHandler)
        {
            try
            {
                this.logger.LogInformation(StringResources.QueueListenerScopedProcessingServiceWorking);
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
            this.logger.LogInformation(StringResources.QueueListenerScopedProcessingServiceStopping);
            this.queueListener.Stop();
        }
    }
}
