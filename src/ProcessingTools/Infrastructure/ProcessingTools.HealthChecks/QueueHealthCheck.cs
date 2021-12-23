// <copyright file="QueueHealthCheck.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.HealthChecks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using ProcessingTools.Contracts.Services.Cache;

    /// <summary>
    /// Queue health check.
    /// </summary>
    public class QueueHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Name of the health check.
        /// </summary>
        public const string HealthCheckName = "queue";

        private readonly IMessageCacheService messageCacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueHealthCheck"/> class.
        /// </summary>
        /// <param name="messageCacheService">Instance of <see cref="IMessageCacheService"/>.</param>
        public QueueHealthCheck(IMessageCacheService messageCacheService)
        {
            this.messageCacheService = messageCacheService ?? throw new ArgumentNullException(nameof(messageCacheService));
        }

        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            string message = this.messageCacheService.Message;

            if (message == "OK")
            {
                return Task.FromResult(HealthCheckResult.Healthy(message));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy(message, exception: this.messageCacheService.Exception));
        }
    }
}
