// <copyright file="LoggingHealthCheckPublisher.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

// See https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.0.
namespace ProcessingTools.HealthChecks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Health check publisher with logging.
    /// </summary>
    public class LoggingHealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingHealthCheckPublisher"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public LoggingHealthCheckPublisher(ILogger<LoggingHealthCheckPublisher> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report?.Status == HealthStatus.Healthy)
            {
                this.logger.LogInformation($"{report?.Status}");
            }
            else
            {
                this.logger.LogError($"{report?.Status}");
            }

            cancellationToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }
    }
}
