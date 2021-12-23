// <copyright file="QueuedHostedService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.TasksServer.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Services;

    /// <summary>
    /// Queued hosted service.
    /// </summary>
    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueuedHostedService"/> class.
        /// </summary>
        /// <param name="taskQueue">Task queue.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        public QueuedHostedService(IBackgroundTaskQueue taskQueue, ILoggerFactory loggerFactory)
        {
            this.TaskQueue = taskQueue ?? throw new ArgumentNullException(nameof(taskQueue));
            this.logger = loggerFactory.CreateLogger<QueuedHostedService>();
        }

        /// <summary>
        /// Gets the task queue.
        /// </summary>
        public IBackgroundTaskQueue TaskQueue { get; }

        /// <inheritdoc/>
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Queued Hosted Service is starting.");

            stoppingToken.Register(() => this.logger.LogDebug($"Queued Hosted Service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await this.TaskQueue.DequeueAsync(stoppingToken).ConfigureAwait(false);

                try
                {
                    await workItem(stoppingToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, $"Error occurred executing {nameof(workItem)}.");
                }
            }

            this.logger.LogInformation("Queued Hosted Service is stopping.");
        }
    }
}
