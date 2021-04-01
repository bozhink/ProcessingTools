// <copyright file="QueueListenerScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Handled exception")]
        public void Start(Action<Exception> exceptionHandler)
        {
            if (exceptionHandler is null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler));
            }

            try
            {
                this.logger.LogInformation(StringResources.QueueListenerScopedProcessingServiceStarting);
                this.queueListener.Start(exceptionHandler);
            }
            catch (Exception ex)
            {
                exceptionHandler.Invoke(ex);
            }
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Handled exception")]
        public void DoWork(Action<Exception> exceptionHandler)
        {
            if (exceptionHandler is null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler));
            }

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Handled exception")]
        public void Stop(Action<Exception> exceptionHandler)
        {
            if (exceptionHandler is null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler));
            }

            try
            {
                this.logger.LogInformation(StringResources.QueueListenerScopedProcessingServiceStopping);
                this.queueListener.Stop(exceptionHandler);
            }
            catch (Exception ex)
            {
                exceptionHandler.Invoke(ex);
            }
        }
    }
}
